namespace server.DTOs;

public class TransactionEventDto
{
    public string EventType { get; set; } = string.Empty;
    public int? PurchaseId { get; set; }
    public int UserId { get; set; }
    public int GiftId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
