using System.ComponentModel.DataAnnotations;

public class GiftCreateDto
{
    [Required, MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required, Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public int DonorId { get; set; } 
}

public class GiftUpdateDto
{
    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }
}
public class GiftResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int DonorId { get; set; } 
}