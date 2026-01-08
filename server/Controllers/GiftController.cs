using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;
using server.DTOs;
using server.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;


namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GiftController : ControllerBase
{
    private readonly IGiftService _giftService;
    private readonly ILogger<GiftController> _logger;

    public GiftController(IGiftService giftService, ILogger<GiftController> logger)
    {
        _giftService = giftService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GiftResponseDto>>> GetAll(PriceSort sort)
    {
        var gifts = await _giftService.GetAllGiftsAsync(sort);
        return Ok(gifts);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<GiftResponseDto>> GetById(int id)
    {
        var gift = await _giftService.GetGiftByIdAsync(id);
        return Ok(gift);
    }

    [HttpGet("byCategory/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
        => Ok(await _giftService.GetByGiftByCategoryAsync(categoryId));


    [HttpGet("byDonor/{donorId:int}")]
    public async Task<IActionResult> GetByDonor(int donorId)
        => Ok(await _giftService.GetByDonorAsync(donorId));

    // [Authorize(Roles = "Admin")]
    // [HttpPost]
    // public async Task<ActionResult<GiftResponseDto>> Create([FromBody] GiftCreateDto dto)
    // {
    //     var gift = await _giftService.AddGiftAsync(dto);
    //     return CreatedAtAction(nameof(GetById), new { id = gift.Id }, gift);
    // }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<GiftResponseDto>> CreateWithImage([FromForm] GiftCreateWithImageDto dto)
    {
        var gift = await _giftService.AddGiftAsync(dto);
        Console.WriteLine("❤️",gift.ImageUrl);

        return CreatedAtAction(nameof(GetById), new { id = gift.Id }, gift);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<GiftResponseDto>> Update(int id, [FromForm] GiftUpdateWithImageDto dto)
    {
        var gift = await _giftService.UpdateGiftAsync(id, dto);
        return Ok(gift);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _giftService.DeleteGiftAsync(id);
        return NoContent();
    }

}