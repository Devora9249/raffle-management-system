using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<PurchaseModel?> GetByIdAsync(int id);
        Task<List<PurchaseModel>> GetAllAsync();
        Task<List<PurchaseModel>> GetByGiftAsync(int giftId);
        Task<List<PurchaseModel>> GetUserCartAsync(int userId); // Draft
        Task AddAsync(PurchaseModel purchase);
        // void Update(PurchaseModel purchase);
        // void Delete(PurchaseModel purchase);
        // Task SaveChangesAsync();
    }
}
