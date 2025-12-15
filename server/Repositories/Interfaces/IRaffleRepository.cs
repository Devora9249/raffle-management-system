using server.Models;
using server.Models.Enums;

namespace server.Repositories.Interfaces
{
    public interface IRaffleRepository
    {
        //CRUD
        Task<IEnumerable<GiftModel>> GetAllRafflesAsync();
        Task<GiftModel?> GetRaffleByIdAsync(int id);
        Task<GiftModel> AddRaffleAsync(RaffleModel raffle);
        Task<GiftModel> UpdateRaffleAsync(RaffleModel raffle);
        Task<bool> DeleteRaffleAsync(int id);

        //בנוסף
        Task <IEnumerable<GiftModel>> FilterByGiftName(string name);
        Task <IEnumerable<GiftModel>> FilterByGiftDonor(string name);        


    }
}