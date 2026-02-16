using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;
using server.Models;
using server.Models.Enums;
using server.Repositories.Implementations;
using AutoMapper;
using Microsoft.Extensions.Logging;

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
        if (winning == null)
        {
            _logger.LogWarning("Winning entry with ID {WinningId} was not found.", id);
            throw new KeyNotFoundException($"Winning {id} not found");
        }

        return _mapper.Map<WinningResponseDto>(winning);
    }

    public async Task<WinningResponseDto> AddWinningAsync(WinningCreateDto dto)
    {
        _logger.LogInformation("Manually adding winning for Gift ID {GiftId}, Winner ID {WinnerId}", dto.GiftId, dto.WinnerId);
        
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
        _logger.LogInformation("Updating winning ID {WinningId}", id);

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
        _logger.LogInformation("Deleting winning entry ID {WinningId}", id);
        var winning = await _winningRepository.DeleteWinningAsync(id);
        if (!winning)
        {
            _logger.LogWarning("Failed to delete winning ID {WinningId}: Not found.", id);
            throw new KeyNotFoundException($"Winning {id} not found");
        }
    }

    public async Task<IEnumerable<WinningResponseDto>> RaffleAsync()
    {
        _logger.LogInformation("Starting bulk raffle process for all available gifts.");
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var rng = new Random();

            var gifts = (await _giftRepository.GetGiftsAsync(PriceSort.None))
                .Where(g => !g.HasWinning)
                .ToList();

            _logger.LogInformation("Found {Count} gifts available for raffle.", gifts.Count);

            var giftIds = gifts.Select(g => g.Id).ToList();
            var purchases = await _purchaseRepository.GetByGiftIdsAsync(giftIds);

            var purchasesByGift = purchases
                .GroupBy(p => p.GiftId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var winningsToEmail = new List<(int GiftId, int WinnerId)>();

            foreach (var gift in gifts)
            {
                if (!purchasesByGift.TryGetValue(gift.Id, out var giftPurchases) || giftPurchases.Count == 0)
                {
                    _logger.LogWarning("Raffle skipped for Gift {GiftId} ({Description}): No purchases found.", gift.Id, gift.Description);
                    continue;
                }

                var winnerUserId = giftPurchases[rng.Next(giftPurchases.Count)].UserId;

                await _winningRepository.AddWinningAsync(new WinningModel
                {
                    GiftId = gift.Id,
                    WinnerId = winnerUserId
                });

                await _giftService.MarkGiftAsHavingWinningAsync(gift.Id);
                _logger.LogInformation("RAFFLE SUCCESS: User {WinnerId} won Gift {GiftId} ({Description})", winnerUserId, gift.Id, gift.Description);

                winningsToEmail.Add((gift.Id, winnerUserId));
            }

            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Bulk raffle transaction committed successfully.");

            foreach (var (giftId, winnerId) in winningsToEmail)
            {
                try
                {
                    await _emailService.SendWinningEmailAsync(giftId, winnerId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send winning email for GiftId={GiftId}, WinnerId={WinnerId}", giftId, winnerId);
                }
            }

            return await GetAllWinningsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "CRITICAL ERROR during bulk raffle. Rolling back transaction.");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<WinningResponseDto?> RaffleSingleGiftAsync(int giftId)
    {
        _logger.LogInformation("Starting single raffle for Gift ID {GiftId}", giftId);
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var gift = await _giftRepository.GetGiftByIdAsync(giftId);
            if (gift == null) throw new KeyNotFoundException($"Gift {giftId} not found");

            if (gift.HasWinning)
            {
                _logger.LogWarning("Raffle aborted: Gift {GiftId} already has a winner.", giftId);
                return null;
            }

            var purchases = await _purchaseRepository.GetByGiftIdsAsync(new List<int> { giftId });
            var purchasesList = purchases.ToList();

            if (!purchasesList.Any())
            {
                _logger.LogWarning("Raffle aborted: No purchases found for Gift {GiftId}.", giftId);
                return null;
            }

            var rng = new Random();
            var winnerUserId = purchasesList[rng.Next(purchasesList.Count)].UserId;

            var winning = new WinningModel { GiftId = giftId, WinnerId = winnerUserId };

            await _winningRepository.AddWinningAsync(winning);
            await _giftService.MarkGiftAsHavingWinningAsync(giftId);

            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Single raffle committed: User {WinnerId} won Gift {GiftId}", winnerUserId, giftId);

            try
            {
                await _emailService.SendWinningEmailAsync(giftId, winnerUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email failed after single raffle for Gift {GiftId}", giftId);
            }

            return await GetWinningByIdAsync(winning.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during single raffle for Gift {GiftId}. Rolling back.", giftId);
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<decimal> GetTotalIncome()
    {
        _logger.LogInformation("Calculating total income from completed purchases.");
        var purchases = await _purchaseRepository.GetAllAsync();
        return purchases
            .Where(p => p.Status == Status.Completed)
            .Sum(p => p.Qty * (p.Gift?.Price ?? 0m));
    }
}