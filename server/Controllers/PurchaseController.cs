using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _service;

    public PurchaseController(IPurchaseService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PurchaseResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());



    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var purchase = await _service.GetByIdAsync(id);
        return Ok(purchase);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PurchaseCreateDto dto)
    {
        var created = await _service.AddAsync(dto);
        return Ok(created);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PurchaseUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }



    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound(new { message = $"Purchase with ID {id} not found." });
    }

    [HttpGet("byGift/{giftId:int}")]
    public async Task<IActionResult> GetByGift(int giftId)
        => Ok(await _service.GetByGiftAsync(giftId));
        

    [HttpGet("count-by-gift")]
    public async Task<IActionResult> GetPurchaseCountByGift()
    => Ok(await _service.GetPurchaseCountByGiftAsync());

}

