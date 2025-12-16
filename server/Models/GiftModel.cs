namespace server.Models
{
    public class GiftModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int DonorId { get; set; }
        public DonorModel Donor { get; set; }
        public CategoryModel Category { get; set; }

        
        // public int RaffleId { get; set; }
        // public RaffleModel Raffle { get; set; }
    }
}