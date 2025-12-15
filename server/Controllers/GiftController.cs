using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;
using server.DTOs;
using server.Models.Enums;


namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GiftController : ControllerBase
{
    private readonly IGiftService _giftService;

    public GiftController(IGiftService giftService)
    {
        _giftService = giftService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GiftResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GiftResponseDto>>> GetAll(PriceSort sort)
    {
        var gifts = await _giftService.GetAllGiftsAsync(sort);
        return Ok(gifts);
    }

[HttpGet("{id}")]
    [ProducesResponseType(typeof(GiftResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftResponseDto>> GetById(int id)
    {
        var gift = await _giftService.GetGiftByIdAsync(id);
        if (gift == null)
        {
            return NotFound(new { message = $"Gift with ID {id} not found." });
        }

        return Ok(gift);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GiftResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GiftResponseDto>> Create([FromBody] GiftResponseDto GiftDto)
    {
        try
        {
            var gift = await _giftService.AddGiftAsync(GiftDto);
            return Ok(gift);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GiftResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GiftResponseDto>> Update( [FromBody] GiftResponseDto updateDto)
    {
        try
        {
            var gift = await _giftService.UpdateGiftAsync(updateDto);
            return Ok(gift);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(int id)
    {
        return await _giftService.DeleteGiftAsync(id);
    }
}
