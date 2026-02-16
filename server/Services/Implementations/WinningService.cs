using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;
using server.Models;
using server.Models.Enums;
using server.Repositories.Implementations;
using AutoMapper;

namespace server.Services.Implementations;

public class WinningService : IWinningService
{
    private readonly IWinningRepository _winningRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IGiftRepository _giftRepository;
    private readonly IEmailService _emailService;
    private readonly IGiftService _giftService;
    private readonly ILogger<WinningService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WinningService(
        IWinningRepository winningRepository,
        IPurchaseRepository purchaseRepository,
        IGiftRepository giftRepository,
        IEmailService emailService,
        IGiftService giftService,
        ILogger<WinningService> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _winningRepository = winningRepository;
        _purchaseRepository = purchaseRepository;
        _giftRepository = giftRepository;
        _emailService = emailService;
        _giftService = giftService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync()
    {
        var winnings = await _winningRepository.GetAllWinningsAsync();

        return _mapper.Map<IEnumerable<WinningResponseDto>>(winnings);

    }

    public async Task<WinningResponseDto> GetWinningByIdAsync(int id)
    {
        var winning = await _winningRepository.GetWinningByIdAsync(id);
        if (winning == null) throw new KeyNotFoundException($"Winning {id} not found");

        return _mapper.Map<WinningResponseDto>(winning);
    }

    public async Task<WinningResponseDto> AddWinningAsync(WinningCreateDto dto)
    {
        var created = await _winningRepository.AddWinningAsync(new WinningModel
        {
            GiftId = dto.GiftId,
            WinnerId = dto.WinnerId
        });

        var full = await _winningRepository.GetWinningByIdAsync(created.Id);
        if (full == null) throw new Exception("Winning was created but could not be loaded.");

        return _mapper.Map<WinningResponseDto>(full);
    }

    public async Task<WinningResponseDto> UpdateWinningAsync(int id, WinningCreateDto dto)
    {
        var model = new WinningModel
        {
            Id = id,
            GiftId = dto.GiftId,
            WinnerId = dto.WinnerId
        };

        var updated = await _winningRepository.UpdateWinningAsync(id, model);
        if (updated == null) throw new KeyNotFoundException($"Winning {id} not found");

        var full = await _winningRepository.GetWinningByIdAsync(updated.Id);
        if (full == null) throw new Exception("Winning was updated but could not be loaded.");

        return _mapper.Map<WinningResponseDto>(full);
    }


    public async Task DeleteWinningAsync(int id)
    {
        var winning = await _winningRepository.DeleteWinningAsync(id);
        if (!winning)
            throw new KeyNotFoundException($"Winning {id} not found");
    }


    

    public async Task<IEnumerable<WinningResponseDto>> RaffleAsync()
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rng = new Random();

            //  מתנות שעדיין לא הוגרלו
            var gifts = (await _giftRepository.GetGiftsAsync(PriceSort.None))
                .Where(g => !g.HasWinning)
                .ToList();

            _logger.LogInformation($"Raffling {gifts.Count} gifts that have not been raffled yet.");

            // שליפת כל הרכישות בבת אחת
            var giftIds = gifts.Select(g => g.Id).ToList();

            var purchases = await _purchaseRepository.GetByGiftIdsAsync(giftIds);

            var purchasesByGift = purchases
                .GroupBy(p => p.GiftId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var winningsToEmail = new List<(int GiftId, int WinnerId)>();

            foreach (var gift in gifts)
            {
                if (!purchasesByGift.TryGetValue(gift.Id, out var giftPurchases))
                {
                    _logger.LogInformation($"No purchases found for gift {gift.Id} - {gift.Description}. Skipping raffle for this gift.");
                    continue;
                }

                if (giftPurchases.Count == 0)
                {
                    _logger.LogInformation($"No purchases found for gift {gift.Id} - {gift.Description}. Skipping raffle for this gift.");
                    continue;

                }

                var winnerUserId =
                    giftPurchases[rng.Next(giftPurchases.Count)].UserId;

                // יצירת זכייה
                await _winningRepository.AddWinningAsync(new WinningModel
                {
                    GiftId = gift.Id,
                    WinnerId = winnerUserId
                });

                //  סימון מתנה כהוגרלה
                await _giftService.MarkGiftAsHavingWinningAsync(gift.Id);
                _logger.LogInformation($"User {winnerUserId} won gift {gift.Id} hasWinning: {gift.HasWinning} 🎁");

                winningsToEmail.Add((gift.Id, winnerUserId));
            }

            //  Commit אטומי
            await _unitOfWork.CommitAsync();

            //  Side effects אחרי Commit
            foreach (var (giftId, winnerId) in winningsToEmail)
            {
                try
                {
                    await _emailService.SendWinningEmailAsync(giftId, winnerId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        $"Failed to send winning email. GiftId={giftId}, WinnerId={winnerId}");
                }
            }

            return await GetAllWinningsAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }


    public async Task<WinningResponseDto?> RaffleSingleGiftAsync(int giftId)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var gift = await _giftRepository.GetGiftByIdAsync(giftId);
            if (gift == null)
                throw new KeyNotFoundException($"Gift {giftId} not found");

            if (gift.HasWinning)
            {
                _logger.LogInformation($"Gift {giftId} already has a winning. Skipping.");
                return null;
            }

            var purchases = await _purchaseRepository.GetByGiftIdsAsync(new List<int> { giftId });

            if (purchases.Count() == 0)
            {
                _logger.LogInformation($"No purchases for gift {giftId}. Skipping raffle.");
                return null;
            }

            var rng = new Random();
            var purchasesList = purchases.ToList();

            var winnerUserId =
                purchasesList[rng.Next(purchasesList.Count)].UserId;


            var winning = new WinningModel
            {
                GiftId = giftId,
                WinnerId = winnerUserId
            };

            await _winningRepository.AddWinningAsync(winning);
            await _giftService.MarkGiftAsHavingWinningAsync(giftId);

            await _unitOfWork.CommitAsync();

            try
            {
                await _emailService.SendWinningEmailAsync(giftId, winnerUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Failed to send winning email. GiftId={giftId}, WinnerId={winnerUserId}");
            }

            return await GetWinningByIdAsync(winning.Id);
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }



    public async Task<decimal> GetTotalIncome()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        return purchases
            .Where(p => p.Status == Status.Completed)
            .Sum(p => p.Qty * (p.Gift?.Price ?? 0m));
    }
}
