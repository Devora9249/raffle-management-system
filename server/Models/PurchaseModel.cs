namespace server.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public int Qty { get; set; }
        public UserModel User { get; set; }
        public GiftModel Gift { get; set; }
    }
}