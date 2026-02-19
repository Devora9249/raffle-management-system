using System.ComponentModel.DataAnnotations;
using server.Models;

namespace server.DTOs
{
    public class PurchaseCreateDto
    {

        [Required]
        public int GiftId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Qty { get; set; }
    }

    public class PurchaseUpdateDto
    {
        

        [Range(1, int.MaxValue)]
        public int? Qty { get; set; }

       
         public Status? Status { get; set; }
    }

    public class PurchaseResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
      
        public string UserName { get; set; } = string.Empty;
        public int GiftId { get; set; }
     
        public string GiftName { get; set; } = string.Empty;
        public int DonorId { get; set; }
        
        public string DonorName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public Status Status { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class GiftPurchaseCountDto
    {
        public int GiftId { get; set; }
        public string GiftName { get; set; } = string.Empty;
        public int PurchaseCount { get; set; }
        public string DonorName { get; set; } = string.Empty;
    }
}
