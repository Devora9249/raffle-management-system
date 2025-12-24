using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using server.Data;
using server.Models;
using server.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace server.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _context;
        private readonly EmailSettingsOptions _emailSettings;

        public EmailService(AppDbContext context, IOptions<EmailSettingsOptions> options)
        {
            _context = context;
            _emailSettings = options.Value;
        }

        public async Task SendWinningEmailAsync(int giftId, int winnerId)
        {
            var gift = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .FirstOrDefaultAsync(g => g.Id == giftId);

            if (gift == null) throw new KeyNotFoundException("Gift not found");

            var donor = gift.Donor;
            if (donor == null) throw new KeyNotFoundException("Donor not found for this gift");

            var winner = await _context.Users.FirstOrDefaultAsync(u => u.Id == winnerId);
            if (winner == null) throw new KeyNotFoundException("Winner user not found");

            var donorEmail = donor.Email;
            if (string.IsNullOrWhiteSpace(donorEmail))
                throw new InvalidOperationException("לתורם אין Email");
            var winnerEmail = winner.Email;
            if (string.IsNullOrWhiteSpace(winnerEmail))
                throw new InvalidOperationException("לזוכה אין Email");

            var raffleDate = DateTime.Now.ToString("dd/MM/yyyy");

            var subjectDonor = "🎉 המתנה שלך זכתה בהגרלה!";
            var bodyDonor = $@"
שלום {donor.Name},

המתנה שתרמת זכתה בהגרלה 🎉

📦 פרטי המתנה:
תיאור: {gift.Description}
קטגוריה: {gift.Category?.Name}
שווי: {gift.Price} ₪

🏆 פרטי הזוכה:
שם: {winner.Name}

📅 תאריך ההגרלה: {raffleDate}

תודה רבה על התרומה!
";

            var subjectWinner = "🎉 זכית בהגרלה!";
            var bodyWinner = $@"
שלום {winner.Name},

מזל טוב! זכית בהגרלה 🎉

📦 פרטי המתנה:
תיאור: {gift.Description}
קטגוריה: {gift.Category?.Name}
שווי: {gift.Price} ₪

📅 תאריך ההגרלה: {raffleDate}
";

            using var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                EnableSsl = _emailSettings.EnableSSL,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password)
            };

            // לתורם
            var msgDonor = new MailMessage(_emailSettings.Username, donorEmail, subjectDonor, bodyDonor);

            // לזוכה
            var msgWinner = new MailMessage(_emailSettings.Username, winnerEmail, subjectWinner, bodyWinner);

            await smtp.SendMailAsync(msgDonor);
            await smtp.SendMailAsync(msgWinner);
        }
    }
}
