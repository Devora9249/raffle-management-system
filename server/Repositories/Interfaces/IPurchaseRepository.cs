using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        // CRUD
        Task<List<PurchaseModel>> GetAllAsync();
        Task<PurchaseModel?> GetByIdAsync(int id);
        Task<PurchaseModel> AddAsync(PurchaseModel purchase);
        Task<PurchaseModel> UpdateAsync(PurchaseModel purchase);
        Task<bool> DeleteAsync(int id);

        // מיוחדים
        Task<List<PurchaseModel>> GetByGiftAsync(int giftId);   // רוכשים לפי מתנה
        Task<List<PurchaseModel>> GetUserCartAsync(int userId); // סל (Draft)
        Task<int> CheckoutAsync(int userId);

    }
}
