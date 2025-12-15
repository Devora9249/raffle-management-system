using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repo;

        public PurchaseService(IPurchaseRepository repo)
        {
            _repo = repo;
        }

        public Task<List<PurchaseModel>> GetAllAsync()
            => _repo.GetAllAsync();

        public Task<PurchaseModel?> GetByIdAsync(int id)
            => _repo.GetByIdAsync(id);

        public async Task<PurchaseModel> AddAsync(PurchaseModel purchase)
        {
            if (purchase.Qty <= 0)
                throw new ArgumentException("Qty must be greater than 0");

            return await _repo.AddAsync(purchase);
        }

        public async Task<PurchaseModel> UpdateAsync(PurchaseModel purchase)
        {
            if (purchase.Qty <= 0)
                throw new ArgumentException("Qty must be greater than 0");

            return await _repo.UpdateAsync(purchase);
        }

        public Task<bool> DeleteAsync(int id)
            => _repo.DeleteAsync(id);

        public Task<List<PurchaseModel>> GetByGiftAsync(int giftId)
            => _repo.GetByGiftAsync(giftId);

        public Task<List<PurchaseModel>> GetUserCartAsync(int userId)
            => _repo.GetUserCartAsync(userId);
            
         public async Task<int> CheckoutAsync(int userId)
            => await _repo.CheckoutAsync(userId);
    }
}