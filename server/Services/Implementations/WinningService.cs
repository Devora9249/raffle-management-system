using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;
using server.Models;
using server.Models.Enums;

namespace server.Services.Implementations;

public class WinningService : IWinningService
{
    private readonly IWinningRepository _winningRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IGiftRepository _giftRepository;
    private readonly IEmailService _emailService;

    public WinningService(
        IWinningRepository winningRepository,
        IPurchaseRepository purchaseRepository,
        IGiftRepository giftRepository,
        IEmailService emailService)
    {
        _winningRepository = winningRepository;
        _purchaseRepository = purchaseRepository;
        _giftRepository = giftRepository;
        _emailService = emailService;
    }

    public async Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync()
    {
        var winnings = await _winningRepository.GetAllWinningsAsync();

        return winnings.Select(w => new WinningResponseDto
        {
            Id = w.Id,
            GiftId = w.GiftId,
            giftName = w.Gift?.Description ?? "",
            WinnerId = w.WinnerId,
            winnerName = w.User?.Name ?? ""
        });
    }

    public async Task<WinningResponseDto> GetWinningByIdAsync(int id)
    {
        var winning = await _winningRepository.GetWinningByIdAsync(id);
        if (winning == null) throw new KeyNotFoundException($"Winning {id} not found");

        return new WinningResponseDto
        {
            Id = winning.Id,
            GiftId = winning.GiftId,
            giftName = winning.Gift?.Description ?? "",
            WinnerId = winning.WinnerId,
            winnerName = winning.User?.Name ?? ""
        };
    }

    public async Task<WinningResponseDto> AddWinningAsync(WinningCreateDto dto)
    {
        var created = await _winningRepository.AddWinningAsync(new WinningModel
        {
            GiftId = dto.GiftId,
            WinnerId = dto.WinnerId
        });

        var full = await _winningRepository.GetWinningByIdAsync(created.Id);
        // if (full == null) throw new Exception("Winning was created but could not be loaded.");

        return new WinningResponseDto
        {
            Id = full.Id,
            GiftId = full.GiftId,
            giftName = full.Gift?.Description ?? "",
            WinnerId = full.WinnerId,
            winnerName = full.User?.Name ?? ""
        };
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
        // if (full == null) throw new Exception("Winning was updated but could not be loaded.");

        return new WinningResponseDto
        {
            Id = full.Id,
            GiftId = full.GiftId,
            giftName = full.Gift?.Description ?? "",
            WinnerId = full.WinnerId,
            winnerName = full.User?.Name ?? ""
        };
    }


    public async Task DeleteWinningAsync(int id)
    {
        var winning = await _winningRepository.DeleteWinningAsync(id);
        if (!winning)
            throw new KeyNotFoundException($"Winning {id} not found");
    }


    public async Task<IEnumerable<WinningResponseDto>> RaffleAsync()
    {
        var gifts = await _giftRepository.GetGiftsAsync(PriceSort.None);
        var rng = new Random();

        foreach (var gift in gifts)
        {
            var purchases = (await _purchaseRepository.GetByGiftAsync(gift.Id)).ToList();

            if (purchases.Count == 0) continue;

            var winnerUserId =
                purchases[rng.Next(purchases.Count)].UserId;

            await _winningRepository.AddWinningAsync(new WinningModel
            {
                GiftId = gift.Id,
                WinnerId = winnerUserId
            });

            try
            {
                await _emailService.SendWinningEmailAsync(gift.Id, winnerUserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return await GetAllWinningsAsync();
    }


    public async Task<decimal> GetTotalIncome()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        return purchases
            .Where(p => p.Status == Status.Completed)
            .Sum(p => p.Qty * (p.Gift?.Price ?? 0m));
    }
}
