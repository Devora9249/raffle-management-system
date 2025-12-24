using server.Models;

namespace server.Repositories.Interfaces;

public interface IWinningRepository
{
    Task<IEnumerable<WinningModel>> GetAllWinningsAsync();
    Task<WinningModel?> GetWinningByIdAsync(int id);
    Task<WinningModel> AddWinningAsync(WinningModel winning);
    Task<WinningModel?> UpdateWinningAsync(int id, WinningModel winning);

    Task<bool> DeleteWinningAsync(int id);


}