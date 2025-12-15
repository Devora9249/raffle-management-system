    using server.Models;
    using server.Models.Enums;

    namespace server.Services.Interfaces
    {
        public interface IGiftService
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


    using server.Models;

// namespace server.Services.Interfaces
// {
//     public interface IGiftService
//     {
//         Task<IEnumerable<GiftModel>> GetAllGiftsAsync();
//         Task<GiftModel?> GetGiftByIdAsync(int id);
//         Task<GiftModel> AddGiftAsync(GiftModel gift);
//         Task<GiftModel> UpdateGiftAsync(GiftModel gift);
//         Task<bool> DeleteGiftAsync(int id);

//         Task<IEnumerable<GiftModel>> SearchByNameAsync(string name);
//         Task<IEnumerable<GiftModel>> FilterByDonorAsync(int donorId);
//         Task<IEnumerable<GiftModel>> SortByPriceAsync(bool ascending);
//     }
// }
