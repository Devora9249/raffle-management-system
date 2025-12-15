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


        }
    }