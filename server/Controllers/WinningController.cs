using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services.Implementations;
using server.Services.Interfaces;


namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WinningController : ControllerBase
{
    private readonly IWinningService _winningService;

    public WinningController(IWinningService winningService)
    {
        _winningService = winningService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync()
    {
        return await _winningService.GetAllWinningsAsync();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<WinningResponseDto> GetWinningByIdAsync(int id)
    {
        return await _winningService.GetWinningByIdAsync(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<WinningResponseDto> AddWinningAsync([FromBody] WinningCreateDto dto)
         => await _winningService.AddWinningAsync(dto);

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<WinningResponseDto> UpdateWinningAsync(int id, [FromBody] WinningCreateDto dto)
            => await _winningService.UpdateWinningAsync(id, dto);

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWinningAsync(int id)
    {
        await _winningService.DeleteWinningAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("total-income")]
    public async Task<decimal> GetTotalIncome()
    {
        return await _winningService.GetTotalIncome();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("doRaffle")]
    public async Task<IEnumerable<WinningResponseDto>> DoRaffle()
    {
        return await _winningService.RaffleAsync();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("raffle-single/{giftId}")]
    public async Task<WinningResponseDto?> RaffleSingleGift(int giftId)
    {
        return await _winningService.RaffleSingleGiftAsync(giftId);
    }
}