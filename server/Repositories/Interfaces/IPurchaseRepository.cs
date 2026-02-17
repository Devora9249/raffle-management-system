using server.DTOs;
using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<PurchaseModel>> GetAllAsync();
        Task<IEnumerable<PurchaseModel>> GetByGiftIdsAsync(IEnumerable<int> giftIds);
        Task<PurchaseModel?> GetByIdAsync(int id);

        Task<PurchaseModel> AddAsync(PurchaseModel purchase);
        Task<PurchaseModel?> UpdateAsync(PurchaseModel purchase);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<PurchaseModel>> GetByGiftAsync(int giftId);   // Completed
        Task<List<PurchaseModel>> GetUserCartAsync(int userId); // Draft
        Task<PurchaseModel?> FindDraftByUserAndGift(int userId, int giftId);
        Task<IEnumerable<GiftPurchaseCountDto>> GetPurchaseCountByGiftAsync();
    }
}
