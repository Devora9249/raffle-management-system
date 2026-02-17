namespace server.Services.Interfaces
{
    using server.DTOs;

    public interface IWinningService
    {
        Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync();
        Task<WinningResponseDto> GetWinningByIdAsync(int id);
        Task<WinningResponseDto> AddWinningAsync(WinningCreateDto dto);
        Task<WinningResponseDto> UpdateWinningAsync(int id, WinningCreateDto dto);
        Task DeleteWinningAsync(int id);
        Task<IEnumerable<WinningResponseDto>> RaffleAsync();
        Task<decimal> GetTotalIncome();
        Task<WinningResponseDto?> RaffleSingleGiftAsync(int giftId);



        //מיון לפי מתנה נרכשת ביותר
        Task<IEnumerable<WinningResponseDto>> GetWinningsSortedByMostPurchasedGiftAsync();
        //חיפוש לפי...
        Task<IEnumerable<WinningResponseDto>> SearchWinningsAsync(string? giftName, string? donorName, int? minPurchases);

    }
}