using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs.Donors;
using server.Models;
using server.Models.Enums;
using server.Services.Implementations;
using server.Services.Interfaces;

namespace server.Services
{
    public class DonorService : IDonorService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService authService;
        private readonly IGiftService giftService;
        private readonly ILogger<DonorService> _logger;

        public DonorService(AppDbContext context, IAuthService authService, IGiftService giftService, ILogger<DonorService> logger)
        {
            _context = context;
            this.authService = authService;
            this.giftService = giftService;
            _logger = logger;
        }
        ///מטרה: להביא רשימה של כל המשתמשים שהם תורמים (Role = Donor), עם אפשרות סינון לפי חיפוש ולפי עיר.
        // -------- פעולות אדמין --------
        public async Task<IEnumerable<DonorListItemDto>> GetDonorsAsync(string? search, string? city)
        {
            var q = _context.Users
                .Where(u => u.Role == RoleEnum.Donor)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                q = q.Where(u =>
                    u.Name.Contains(search) ||
                    u.Email.Contains(search) ||
                    u.Phone.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                city = city.Trim();
                q = q.Where(u => u.City.Contains(city));
            }

            return await q
                .OrderBy(u => u.Name)
                .Select(u => new DonorListItemDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    City = u.City,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DonorWithGiftsDto>> GetDonorsWithGiftsAsync()
        {
            var donors = await GetDonorsAsync("", "");

            var gifts = await giftService.GetAllGiftsAsync(PriceSort.None, null, null);

            return donors.Select(d => new DonorWithGiftsDto
            {
                DonorId = d.Id,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                City = d.City,
                Address = d.Address,
                Gifts = gifts
                    .Where(g => g.DonorId == d.Id)
                    .ToList()
            });
        }

        ///מטרה: לשנות תפקיד (Role) למשתמש מסוים — פעולה של אדמין.
        public async Task SetUserRoleAsync(int userId, RoleEnum role)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Role = role;
            await _context.SaveChangesAsync();
        }

        // -------- דשבורד לתורם --------
        ///מטרה: להחזיר “דשבורד לתורם” — נתונים מסכמים לתורם ספציפי: מתנות שלו, כמות כרטיסים שנמכרו, כמה קונים ייחודיים לכל מתנה, והאם יש זכייה.
        public async Task<DonorDashboardResponseDto> GetDonorDashboardAsync(int donorId)
        {
            var donor = await _context.Users.FirstOrDefaultAsync(u => u.Id == donorId);
            if (donor == null) throw new KeyNotFoundException("Donor user not found");

            // 1) מתנות של התורם
            var gifts = await _context.Gifts
                .Where(g => g.DonorId == donorId)
                .Select(g => new { g.Id, g.Description })
                .ToListAsync();

            var giftIds = gifts.Select(g => g.Id).ToList();

            // 2) סטטיסטיקת רכישות Completed בלבד
            var purchaseStats = await _context.Purchases
                .Where(p => giftIds.Contains(p.GiftId) && p.Status == Status.Completed)
                .GroupBy(p => p.GiftId)
                .Select(grp => new
                {
                    GiftId = grp.Key,
                    TicketsSold = grp.Sum(x => x.Qty), // ✅ אצלך זה Qty
                    UniqueBuyers = grp.Select(x => x.UserId).Distinct().Count()
                })
                .ToListAsync();

            // 3) זכיות לפי GiftId
            var winningGiftIds = await _context.Winnings
                .Where(w => giftIds.Contains(w.GiftId))
                .Select(w => w.GiftId)
                .Distinct()
                .ToListAsync();

            int Tickets(int giftId) =>
                purchaseStats.FirstOrDefault(x => x.GiftId == giftId)?.TicketsSold ?? 0;

            int Buyers(int giftId) =>
                purchaseStats.FirstOrDefault(x => x.GiftId == giftId)?.UniqueBuyers ?? 0;

            return new DonorDashboardResponseDto
            {
                DonorId = donor.Id,
                DonorName = donor.Name,

                TotalGifts = gifts.Count,
                TotalTicketsSold = purchaseStats.Sum(x => x.TicketsSold),
                TotalUniqueBuyers = purchaseStats.SelectMany(x => new int[] { x.UniqueBuyers }).Sum(), // סה"כ ייחודיים לכל מתנה (אפשר גם אחרת)

                Gifts = gifts.Select(g => new DonorGiftStatsDto
                {
                    GiftId = g.Id,
                    Description = g.Description,
                    TicketsSold = Tickets(g.Id),
                    UniqueBuyers = Buyers(g.Id),
                    HasWinning = winningGiftIds.Contains(g.Id)
                }).ToList()
            };
        }

        public async Task<DonorListItemDto?> GetDonorDetailsAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId && u.Role == RoleEnum.Donor)
                .Select(u => new DonorListItemDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    City = u.City
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddDonorAsync(addDonorDto donorDto)
        {
            var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == donorDto.Email);

            if (existingUser != null)
            {
                return false;
            }

            var newDonor = new UserModel
            {
                Name = donorDto.Name,
                Email = donorDto.Email,
                Phone = donorDto.Phone,
                City = donorDto.City,
                Address = donorDto.Address,
                Role = RoleEnum.Donor,
                IsActive = true
            };

            //  Hash לסיסמה
            newDonor.Password = authService.HashPassword(donorDto.Password);

            _context.Users.Add(newDonor);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Added new donor: {@DonorDto}", donorDto);


            return true;
        }
    }
}