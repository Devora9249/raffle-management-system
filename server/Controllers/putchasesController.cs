using Microsoft.AspNetCore.Mvc;
using server.Models;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var purchase = await _service.GetByIdAsync(id);
        ///////////////
        return purchase == null ? NotFound() : Ok(purchase);
        ///////////////
    }

    [HttpPost]
    public async Task<IActionResult> Create(PurchaseModel purchase)
    {
        var created = await _service.AddAsync(purchase);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    public async Task<IActionResult> Update(PurchaseModel purchase)
        => Ok(await _service.UpdateAsync(purchase));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    // פרטי רוכשים לפי מתנה
    [HttpGet("byGift/{giftId}")]
    public async Task<IActionResult> GetByGift(int giftId)
        => Ok(await _service.GetByGiftAsync(giftId));

    // סל של משתמש
    [HttpGet("cart/{userId}")]
    public async Task<IActionResult> GetUserCart(int userId)
        => Ok(await _service.GetUserCartAsync(userId));

[HttpPost("checkout/{userId}")]
public async Task<IActionResult> Checkout(int userId)
{
    var completedCount = await _service.CheckoutAsync(userId);

    if (completedCount == 0)
        return BadRequest(new { message = "Cart is empty" });

    return Ok(new
    {
        message = "Checkout completed successfully",
        itemsCompleted = completedCount
    });
}
}