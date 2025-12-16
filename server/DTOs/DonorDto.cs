namespace server.DTOs.Donors
{
    public class DonorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
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

    public class DonorGiftStatsDto
    {
        public int GiftId { get; set; }
        public string Description { get; set; } = string.Empty;

        public int TicketsSold { get; set; }
        public int UniqueBuyers { get; set; }

        public bool HasWinning { get; set; }
    }
}
