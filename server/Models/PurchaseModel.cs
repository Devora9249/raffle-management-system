namespace server.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public int Qty { get; set; }
        public Status Status { get; set; } = Status.Draft;
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public UserModel User { get; set; }=default!;
        public GiftModel Gift { get; set; }=default!;
    }

    public enum Status
    {
        Draft,
        Completed,
    }
}