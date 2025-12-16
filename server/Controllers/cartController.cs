using Microsoft.AspNetCore.Mvc;
using server.DTOs.Cart;
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
    [ProducesResponseType(typeof(CartItemResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CartAddDto dto)
    {
        try
        {
            var created = await _service.AddToCartAsync(dto);
            return Ok(created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(CartItemResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateQty([FromBody] CartUpdateDto dto)
    {
        try
        {
            var updated = await _service.UpdateQtyAsync(dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Cart item not found." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{purchaseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(int purchaseId)
    {
        try
        {
            var ok = await _service.RemoveAsync(purchaseId);
            return ok ? NoContent() : NotFound(new { message = "Cart item not found." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("checkout/{userId:int}")]
    [ProducesResponseType(typeof(CartCheckoutResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Checkout(int userId)
        => Ok(await _service.CheckoutAsync(userId));
}
