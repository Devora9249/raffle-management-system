namespace server.Services.Interfaces
{
    public interface IWinningService
    {
        Task SendWinningEmailAsync(int giftId, int winnerId);
    }
}
