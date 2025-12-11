namespace server.Models
{
    public class GiftModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public int DonorId { get; set; }
        public DonorModel Donor { get; set; }
        public CategoryModel Category { get; set; }
    }
}