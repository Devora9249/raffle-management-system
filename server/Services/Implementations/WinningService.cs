using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using server.Data;
using server.Services.Interfaces;
using server.Services.Options;
using System.Net;
using System.Net.Mail;

namespace server.Services
{
    public class WinningService : IWinningService
    {
        private readonly AppDbContext _context;
        private readonly EmailSettingsOptions _emailSettings;

        public WinningService(
    AppDbContext context,
    IOptions<EmailSettingsOptions> options)
{
    _context = context;
    _emailSettings = options.Value;
}

        public async Task SendWinningEmailAsync(int giftId, int winnerId)
        {
            // שליפת הזכייה + מתנה + תורם + זוכה
            var winning = await _context.Winnings
                .Include(w => w.Gift)
                    .ThenInclude(g => g.Donor)
                .Include(w => w.Gift)
                    .ThenInclude(g => g.Category)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.GiftId == giftId && w.WinnerId == winnerId);

            if (winning == null)
                throw new Exception("זכייה לא נמצאה עבור giftId+winnerId הללו");

            if (winning.Gift?.Donor == null)
                throw new Exception("למתנה אין תורם מחובר (Gift.Donor null)");

            // יש הגרלה אחת → התאריך הוא עכשיו
            var raffleDate = DateTime.Now.ToString("dd/MM/yyyy");

            var donor = winning.Gift.Donor;
            var gift = winning.Gift;
            var winner = winning.User;

            // ודאי שיש לתורם מייל
            // (אם אצלך זה נקרא אחרת מ-Email/Name, תשני פה בהתאם)
            var toEmail = donor.Email;
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new Exception("לתורם אין Email");

            var subject = "🎉 זכייה בהגרלה - המתנה שלך זכתה!";
            var body = $@"
שלום {donor.Name},

המתנה שתרמת זכתה בהגרלה 🎉

📦 פרטי המתנה:
תיאור: {gift.Description}
קטגוריה: {gift.Category?.Name}
שווי: {gift.Price} ₪

🏆 פרטי הזוכה:
שם: {winner?.Name}

📅 תאריך ההגרלה: {raffleDate}

תודה רבה על התרומה!
";

            await SendEmailAsync(toEmail, subject, body);
        }

        private async Task SendEmailAsync(string to, string subject, string body)
{
    var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
    {
        EnableSsl = _emailSettings.EnableSSL,
        Credentials = new NetworkCredential(
            _emailSettings.Username,
            _emailSettings.Password)
    };

    var mail = new MailMessage
    {
        From = new MailAddress(_emailSettings.Username),
        Subject = subject,
        Body = body,
        IsBodyHtml = false
    };

    mail.To.Add(to);
    mail.Add("devora.video@gmail.com");
    mail.Add("potat4241@gmail.com")

    await smtp.SendMailAsync(mail);
}
    }
}
