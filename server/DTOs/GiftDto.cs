using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class GiftResponseDto
{
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int DonorId { get; set; }
}
