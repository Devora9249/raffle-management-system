using System.ComponentModel.DataAnnotations;

namespace server.DTOs;
public class WinningResponseDto
{
    public int Id { get; set; }
    public int GiftId { get; set; }
    public string giftName { get; set; }
    public int WinnerId { get; set; }
    public string winnerName { get; set; }
}

public class WinningCreateDto
{
   
    [Required]
    public int GiftId { get; set; }

    [Required]
    public int WinnerId { get; set; }
}