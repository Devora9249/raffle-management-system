using Microsoft.AspNetCore.Mvc;
using server.DTOs;
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

    [HttpGet]
    public async Task<IEnumerable<WinningResponseDto>> GetAllWinningsAsync()
    {
        return await _winningService.GetAllWinningsAsync();
    }

    [HttpGet("{id}")]
    public async Task<WinningResponseDto?> GetWinningByIdAsync(int id)
    {
        return await _winningService.GetWinningByIdAsync(id);
    }

    [HttpPost]
    public async Task<WinningCreateDto> AddWinningAsync([FromBody] WinningCreateDto dto)
    {
        return await _winningService.AddWinningAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<WinningCreateDto> UpdateWinningAsync(int id, [FromBody] WinningCreateDto dto)
    {
        return await _winningService.UpdateWinningAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteWinningAsync(int id)
    {
        return await _winningService.DeleteWinningAsync(id);
    }

    [HttpGet("total-income")]
    public async Task<decimal> GetTotalIncome()
    {
        return await _winningService.GetTotalIncome();
    }

    [HttpGet("doRaffle")]
    public async Task<IActionResult> DoRaffle()
    {
        await _winningService.RaffleAsync();
        return Ok();
    }
}