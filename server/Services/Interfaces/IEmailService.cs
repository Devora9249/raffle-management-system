namespace server.Services.Interfaces
{
    using server.Models;
    public interface IEmailService
    {
        // שינוי החתימה לקבלת המודל המלא
       Task SendWinningEmailAsync(int giftId, int winnerId);

    }
}