using System.ComponentModel.DataAnnotations;
using server.Models;


namespace server.DTOs.Donors
{

    public class DonorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        // public Task<List<GiftModel>>? Gifts { get; set; }
    }

    public class DonorWithGiftsDto
    {
        public int DonorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<GiftResponseDto> Gifts { get; set; } = new();
    }

    public class DonorDashboardResponseDto
    {
        public int DonorId { get; set; }
        public string DonorName { get; set; } = string.Empty;

        public int TotalGifts { get; set; }
        public int TotalTicketsSold { get; set; }
        public int TotalUniqueBuyers { get; set; }

        public List<DonorGiftStatsDto> Gifts { get; set; } = new();
    }

    public class addDonorDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(200)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Address { get; set; } = string.Empty;
        
    }

    public class DonorGiftStatsDto
    {
        public int GiftId { get; set; }
        public string Description { get; set; } = string.Empty;

        public int TicketsSold { get; set; }
        public int UniqueBuyers { get; set; }

        public bool HasWinning { get; set; }
    }
}
