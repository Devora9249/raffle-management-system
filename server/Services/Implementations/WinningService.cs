using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;
using server.Models;
using server.Models.Enums;

namespace server.Services.Implementations;

public class WinningService: IWinningService
{
    private readonly IWinningRepository _winningRepository;
    private readonly IPurchaseRepository _PurchaseRepository;
    private readonly IGiftRepository _giftRepository;

    public WinningService(IWinningRepository winningRepository, IPurchaseRepository purchaseRepository, IGiftRepository giftRepository)
    {
        _winningRepository = winningRepository;
        _PurchaseRepository = purchaseRepository;
        _giftRepository = giftRepository;
    }

    public async Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync()
    {
        var winnings = await _winningRepository.GetAllWinningsAsync();

        return winnings.Select(g => new WinningResponseDto
        {
            GiftId = g.GiftId,
            giftName = g.Gift.Description,
            WinnerId = g.WinnerId,
            winnerName = g.User.Name
        });
    }

    public async Task<WinningResponseDto?> GetWinningByIdAsync(int id)
    {
        var winning = await _winningRepository.GetWinningByIdAsync(id);
        return new WinningResponseDto
        {
            GiftId = winning.GiftId,
            giftName = winning.Gift.Description,
            WinnerId = winning.WinnerId,
            winnerName = winning.User.Name
        };
    }

    public async Task<WinningCreateDto> AddWinningAsync(WinningCreateDto dto)
    {
        var model = new WinningModel
        {
            GiftId = dto.GiftId,
            WinnerId = dto.WinnerId
        };

        var createdWinning = await _winningRepository.AddWinningAsync(model);

        return new WinningCreateDto
        {
            Id = createdWinning.Id,
            GiftId = createdWinning.GiftId,
            WinnerId = createdWinning.WinnerId
        };
    }

    public async Task<WinningCreateDto> UpdateWinningAsync(int id, WinningCreateDto dto)
    {
        var model = new WinningModel
        {
            Id = id,
            GiftId = dto.GiftId,
            WinnerId = dto.WinnerId
        };

        var updatedWinning = await _winningRepository.UpdateWinningAsync(model);

        return new WinningCreateDto
        {
            Id = updatedWinning.Id,
            GiftId = updatedWinning.GiftId,
            WinnerId = updatedWinning.WinnerId
        };
    }

    public async Task<bool> DeleteWinningAsync(int id)
    {
        return await _winningRepository.DeleteWinningAsync(id);
    }


    public async Task<IEnumerable<WinningResponseDto>> RaffleAsync()
    {
        var gifts = await _giftRepository.GetAllGiftsAsync(PriceSort.None);

        foreach (var gift in gifts)
        {
            //מקבל רשימת קונים של מתנה מסויימת
            var purchases = await _PurchaseRepository.GetByGiftAsync(gift.Id);
            if (purchases.Count == 0)
                continue;
            //בוחר מנצח אקראי מתוך רשימת הקונים
            var random = new Random();
            var winnerIndex = random.Next(purchases.Count);
            var winnerId = purchases[winnerIndex].Id;

            var winning = new WinningModel
            {
                GiftId = gift.Id,
                WinnerId = winnerId
            };
            //שומר את המנצח בבסיס הנתונים
            await _winningRepository.AddWinningAsync(winning);
        }

        return await GetAllWinningsAsync();
    }

    public async Task<decimal> GetTotalIncome()
    {
        var purchases = await _PurchaseRepository.GetAllAsync();
        return purchases.Sum(p => p.Qty * p.Gift.Price);
    }


}



