using server.DTOs;
using server.Models;
    using server.Models.Enums;

    namespace server.Services.Interfaces
    {
        public interface IGiftService
        {
            //CRUD
            Task<IEnumerable<GiftResponseDto>> GetAllGiftsAsync(PriceSort sort);
            Task<GiftResponseDto?> GetGiftByIdAsync(int id);
            Task<GiftResponseDto> AddGiftAsync(GiftResponseDto gift);
            Task<GiftResponseDto> UpdateGiftAsync(GiftResponseDto gift);
            Task<bool> DeleteGiftAsync(int id);

            //בנוסף
            Task <IEnumerable<GiftResponseDto>> FilterByGiftName(string name);
            Task <IEnumerable<GiftResponseDto>> FilterByGiftDonor(string name);        
            Task<List<GiftResponseDto>> GetByDonorAsync(int donorId);


        }
    }


    // using server.Models;

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
