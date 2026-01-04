using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(IEnumerable<CartItemResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart(int userId)
        => Ok(await _service.GetCartAsync(userId));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CartAddDto dto)
    {
        var created = await _service.AddToCartAsync(dto);
        return Ok(created);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateQty([FromBody] CartUpdateDto dto)
    {
        var updated = await _service.UpdateQtyAsync(dto);
        return Ok(updated);
    }


    [HttpDelete("{purchaseId:int}")]
    public async Task<IActionResult> Remove(int purchaseId)
    {
        await _service.RemoveAsync(purchaseId);
        return NoContent();
    }

    [HttpPost("checkout/{userId:int}")]
    [ProducesResponseType(typeof(CartCheckoutResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Checkout(int userId)
        => Ok(await _service.CheckoutAsync(userId));
}