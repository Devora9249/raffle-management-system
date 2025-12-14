using server.Models;
using server.Models.Enums;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations;

public class GiftService : IGiftService
{
    private readonly IGiftRepository _giftRepository;

    public GiftService(IGiftRepository giftRepository)
    {
        _giftRepository = giftRepository;
    }

    public async Task<IEnumerable<GiftModel>> GetAllGiftsAsync(PriceSort sort)
    {
        return await _giftRepository.GetAllGiftsAsync(sort);
    }

    public async Task<GiftModel?> GetGiftByIdAsync(int id)
    {
        return await _giftRepository.GetGiftByIdAsync(id);
    }

    public async Task<GiftModel> AddGiftAsync(GiftModel gift)
    {
        return await _giftRepository.AddGiftAsync(gift);
    }

    public async Task<GiftModel> UpdateGiftAsync(GiftModel gift)
    {
        return await _giftRepository.UpdateGiftAsync(gift);
    }

    public async Task<bool> DeleteGiftAsync(int id)
    {
        return await _giftRepository.DeleteGiftAsync(id);
    }

    public async Task<IEnumerable<GiftModel>> FilterByGiftName(string name)
    {
        return await _giftRepository.FilterByGiftName(name);
    }

    public async Task<IEnumerable<GiftModel>> FilterByGiftDonor(string name)
    {
        return await _giftRepository.FilterByGiftDonor(name);
    }
}