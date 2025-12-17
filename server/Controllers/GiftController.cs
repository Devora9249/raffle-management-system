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

[HttpGet("byDonor/{donorId:int}")]
[ProducesResponseType(typeof(IEnumerable<GiftResponseDto>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetByDonor(int donorId)
    => Ok(await _giftService.GetByDonorAsync(donorId));

    
    [HttpPost]
[ProducesResponseType(typeof(GiftResponseDto), StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<GiftResponseDto>> Create([FromBody] GiftCreateDto dto)
{
    try
    {
        var gift = await _giftService.AddGiftAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = gift.Id }, gift);
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
public async Task<ActionResult<GiftResponseDto>> Update(int id, [FromBody] GiftUpdateDto dto)
{
    try
    {
        var gift = await _giftService.UpdateGiftAsync(id, dto);
        return Ok(gift);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { message = ex.Message });
    }
    catch (ArgumentException ex)
    {
        return BadRequest(new { message = ex.Message });
    }
}

   [HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Delete(int id)
{
    var ok = await _giftService.DeleteGiftAsync(id);
    return ok ? NoContent() : NotFound(new { message = $"Gift with ID {id} not found." });
}
}