namespace server.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendWinningEmailAsync(int giftId, int winnerId);
    }
}
