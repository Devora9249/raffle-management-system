using server.Models;
using server.Models.Enums;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.DTOs;

namespace server.Services.Implementations;

public class GiftService : IGiftService
{
    private readonly IGiftRepository _giftRepository;

    public GiftService(IGiftRepository giftRepository)
    {
        _giftRepository = giftRepository;
    }

    public async Task<IEnumerable<GiftResponseDto>> GetAllGiftsAsync(PriceSort sort)
    {
        var gifts = await _giftRepository.GetAllGiftsAsync(sort);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }

    public async Task<GiftResponseDto?> GetGiftByIdAsync(int id)
    {
        var gift = await _giftRepository.GetGiftByIdAsync(id);
        if (gift == null)
        {
            return null;
        }
        return new GiftResponseDto
        {
            Id = gift.Id,
            Description = gift.Description,
            CategoryId = gift.CategoryId,
            Price = gift.Price,
            DonorId = gift.DonorId
        };
    }

    public async Task<GiftResponseDto> AddGiftAsync(GiftResponseDto dto)
    {
        var model = new GiftModel
        {
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            DonorId = dto.DonorId
        };

        var createdGift = await _giftRepository.AddGiftAsync(model);

        return new GiftResponseDto
        {
            Id = createdGift.Id,
            Description = createdGift.Description,
            CategoryId = createdGift.CategoryId,
            Price = createdGift.Price,
            DonorId = createdGift.DonorId
        };
    }

    public async Task<GiftResponseDto> UpdateGiftAsync(GiftResponseDto gift)
    {
        var model = new GiftModel
        {
            Id = gift.Id,
            Description = gift.Description,
            CategoryId = gift.CategoryId,
            Price = gift.Price,
            DonorId = gift.DonorId
        };

        var updatedGift = await _giftRepository.UpdateGiftAsync(model);

        return new GiftResponseDto
        {
            Id = updatedGift.Id,
            Description = updatedGift.Description,
            CategoryId = updatedGift.CategoryId,
            Price = updatedGift.Price,
            DonorId = updatedGift.DonorId
        };
    }

    public async Task<bool> DeleteGiftAsync(int id)
    {
        return await _giftRepository.DeleteGiftAsync(id);
    }

    public async Task<IEnumerable<GiftResponseDto>> FilterByGiftName(string name)
    {
        var gifts = await _giftRepository.FilterByGiftName(name);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }

    public async Task<IEnumerable<GiftResponseDto>> FilterByGiftDonor(string name)
    {
        var gifts = await _giftRepository.FilterByGiftDonor(name);

        return gifts.Select(g => new GiftResponseDto
        {
            Id = g.Id,
            Description = g.Description,
            CategoryId = g.CategoryId,
            Price = g.Price,
            DonorId = g.DonorId
        });
    }
    public async Task<List<GiftResponseDto>> GetByDonorAsync(int donorId)
{
    var gifts = await _giftRepository.GetByDonorAsync(donorId);

    return gifts.Select(g => new GiftResponseDto
    {
        Id = g.Id,
        Description = g.Description,
        CategoryId = g.CategoryId,
        Price = g.Price,
        DonorId = g.DonorId
    }).ToList();
}

}