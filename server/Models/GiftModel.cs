namespace server.Models
{
    public class GiftModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int DonorId { get; set; }
        public UserModel Donor { get; set; } = default!;
        public CategoryModel Category { get; set; } = default!;
        public string ImageUrl { get; set; } = string.Empty;
        public Boolean HasWinning { get; set; } = false;

       
    }
}