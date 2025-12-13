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
public PurchaseStatus Status { get; set; } = PurchaseStatus.Cart;
public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public enum PurchaseStatus { Cart, Paid, Cancelled }
}