namespace server.Models
{
    public class RaffleModel
    {
        public int Id { get; set; }
        public DateTime RaffleDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<GiftModel> Gifts { get; set; } = new();
        public List<WinningModel> Winnings { get; set; } = new();
    }
}