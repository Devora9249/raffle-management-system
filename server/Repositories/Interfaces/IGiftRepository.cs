using server.Models;
using server.Models.Enums;

namespace server.Repositories.Interfaces
{
    public interface IGiftRepository
    {
        //CRUD
        Task<IEnumerable<GiftModel>> GetAllGiftsAsync(PriceSort sort);
        Task<GiftModel?> GetGiftByIdAsync(int id);
        Task<GiftModel> AddGiftAsync(GiftModel gift);
        Task<GiftModel> UpdateGiftAsync(GiftModel gift);
        Task<bool> DeleteGiftAsync(int id);

        //בנוסף
        Task <IEnumerable<GiftModel>> FilterByGiftName(string name);
        Task <IEnumerable<GiftModel>> FilterByGiftDonor(string name);        


    }
}

