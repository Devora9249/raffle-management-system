
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
    public int Id { get; set; }
    public int GiftId { get; set; }
    public int WinnerId { get; set; }
}