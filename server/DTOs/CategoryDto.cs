using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CategoryCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
public class CategoryUpdateDto
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }

}

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
