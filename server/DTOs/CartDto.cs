using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CartAddDto
    {
        [Required] public int UserId { get; set; }
        [Required] public int GiftId { get; set; }
        [Required, Range(1, int.MaxValue)] public int Qty { get; set; }
    }

    public class CartUpdateDto
    {
        [Required] public int PurchaseId { get; set; }
        [Required, Range(1, int.MaxValue)] public int Qty { get; set; }
    }

    public class CartItemResponseDto
    {
        public int PurchaseId { get; set; }
        public int GiftId { get; set; }
        public int Qty { get; set; }
        public DateTime AddedAt { get; set; }
    }

    public class CartCheckoutResponseDto
    {
        public int UserId { get; set; }
        public int ItemsCompleted { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
