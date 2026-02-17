using server.DTOs;
using server.Models;
using server.Models.Enums;

namespace server.Repositories.Interfaces
{
    public interface IGiftRepository
    {
        //CRUD
        Task<IEnumerable<GiftModel>> GetAllGiftsAsync(
        int? categoryId,
        int? donorId,
        PriceSort sort);
        Task<IEnumerable<GiftModel>> GetGiftsAsync(PriceSort sort);
        Task<GiftModel?> GetGiftByIdAsync(int id);
        Task<IEnumerable<GiftModel>> GetGiftByCategoryAsync(int categoryId);
        Task<GiftModel> AddGiftAsync(GiftModel gift);
        Task<GiftModel?> UpdateGiftAsync(GiftModel gift);
        Task<bool> HasPurchasesAsync(int productId);
        Task<bool> DeleteGiftAsync(int id);

        //בנוסף
        Task<IEnumerable<GiftModel>> FilterByGiftName(string name);
        Task<IEnumerable<GiftModel>> FilterByGiftDonor(string name);
        Task<IEnumerable<GiftModel>> GetByDonorAsync(int donorId);
        Task<IEnumerable<GiftPurchaseCountDto>> GetPurchaseCountByGiftAsync();
        Task<bool> ExistsByDescriptionAsync(string description);

    }
}

