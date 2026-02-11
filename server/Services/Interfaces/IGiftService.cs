using server.DTOs;
using server.Models;
using server.Models.Enums;

namespace server.Services.Interfaces
{
    public interface IGiftService
    {
        //CRUD
        Task<IEnumerable<GiftResponseDto>> GetAllGiftsAsync(
    PriceSort sort,
    int? categoryId,
    int? donorId);
        Task<IEnumerable<GiftResponseDto>> GetAllAsync(PriceSort sort);
        Task<GiftResponseDto?> GetGiftByIdAsync(int id);
        Task<IEnumerable<GiftResponseDto?>> GetByGiftByCategoryAsync(int categoryId);
        Task<GiftResponseDto> AddGiftAsync(GiftCreateWithImageDto dto);
        Task<GiftResponseDto> UpdateGiftAsync(int id, GiftUpdateWithImageDto dto);
        Task<bool> DeleteGiftAsync(int id);

        //בנוסף
        Task<IEnumerable<GiftResponseDto>> FilterByGiftName(string name);
        Task<IEnumerable<GiftResponseDto>> FilterByGiftDonor(string name);
        Task<IEnumerable<GiftResponseDto>> GetByDonorAsync(int donorId);
        Task MarkGiftAsHavingWinningAsync(int giftId);
        Task<IEnumerable<GiftPurchaseCountDto>> GetPurchaseCountByGiftAsync();

    }
}
