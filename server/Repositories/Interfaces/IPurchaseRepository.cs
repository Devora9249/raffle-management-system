using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<PurchaseModel>> GetAllAsync();
        Task<PurchaseModel?> GetByIdAsync(int id);

        Task<PurchaseModel> AddAsync(PurchaseModel purchase);
        Task<PurchaseModel> UpdateAsync(PurchaseModel purchase);
        Task<bool> DeleteAsync(int id);

        Task<List<PurchaseModel>> GetByGiftAsync(int giftId);   // Completed
        Task<List<PurchaseModel>> GetUserCartAsync(int userId); // Draft
        Task<int> CheckoutAsync(int userId);                    // Draft -> Completed
    }
}
