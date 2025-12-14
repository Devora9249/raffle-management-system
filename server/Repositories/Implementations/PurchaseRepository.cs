using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Repositories.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
     Task<PurchaseModel?> GetByIdAsync(int id);
        Task<List<PurchaseModel>> GetAllAsync();
        Task<List<PurchaseModel>> GetByGiftAsync(int giftId);
        Task<List<PurchaseModel>> GetUserCartAsync(int userId); // Draft
        Task AddAsync(PurchaseModel purchase);
    }
}
