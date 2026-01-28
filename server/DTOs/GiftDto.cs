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
    [MaxLength(1000)]
    public string ImageUrl { get; set; } = string.Empty;
}

public class GiftCreateWithImageDto
{
    [Required, MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required, Range(0.01, double.MaxValue)]

    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int DonorId { get; set; }
    public IFormFile? Image { get; set; } = null!;
}

public class GiftUpdateWithImageDto
{
    [MaxLength(500)]
    public string? Description { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }
    public int? CategoryId { get; set; }
    public int? DonorId { get; set; }
    public IFormFile? Image { get; set; } = null!;
}


public class GiftUpdateDto
{
    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    [MaxLength(1000)]
    public string? ImageUrl { get; set; }
}
public class GiftResponseDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int DonorId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    
}