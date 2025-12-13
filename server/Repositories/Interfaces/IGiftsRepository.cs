using server.Models;

namespace server.Repositories.Interfaces
{
    public interface IGiftsRepository
    {
        Task<IEnumerable<GiftModel>> GetAllGiftsAsync();
        Task<GiftModel?> GetGiftByIdAsync(int id);
        Task<GiftModel> AddGiftAsync(GiftModel gift);
        Task<GiftModel> UpdateGiftAsync(GiftModel gift);
        Task<bool> DeleteGiftAsync(int id);
    }
}