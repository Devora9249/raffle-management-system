using server.Models;

namespace server.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<List<PurchaseModel>> GetAllAsync();
        Task<PurchaseModel?> GetByIdAsync(int id);
        Task<PurchaseModel> AddAsync(PurchaseModel purchase);
        Task<PurchaseModel> UpdateAsync(PurchaseModel purchase);
        Task<bool> DeleteAsync(int id);

        Task<List<PurchaseModel>> GetByGiftAsync(int giftId);
        Task<List<PurchaseModel>> GetUserCartAsync(int userId);
        Task<int> CheckoutAsync(int userId);

    }
}
