using System.ComponentModel.DataAnnotations;
using server.Models;

namespace server.DTOs
{
    public class PurchaseCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GiftId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Qty { get; set; }
    }

    public class PurchaseUpdateDto
    {
        // [Required]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int? Qty { get; set; }

        // אם תרצי לאפשר שינוי סטטוס בקנייה (למשל Admin) - תשאירי כאן:
        // public Status? Status { get; set; }
    }

    public class PurchaseResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GiftId { get; set; }
        public int Qty { get; set; }
        public Status Status { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
