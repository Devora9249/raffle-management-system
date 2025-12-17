namespace server.Services.Interfaces
{
    using server.DTOs;

    public interface IWinningService
    {
        Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync();
        Task<WinningResponseDto?> GetWinningByIdAsync(int id);
        Task<WinningResponseDto> AddWinningAsync(WinningCreateDto dto);
        Task<WinningResponseDto> UpdateWinningAsync(int id, WinningCreateDto dto);
        Task<bool> DeleteWinningAsync(int id);
        Task<IEnumerable<WinningResponseDto>> RaffleAsync();
        Task<decimal> GetTotalIncome();
    }
}