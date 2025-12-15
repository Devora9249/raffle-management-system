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
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllPurchasesAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _service.GetPurchaseByIdAsync(id);
        return p == null ? NotFound() : Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PurchaseModel purchase) => Ok(await _service.AddPurchaseAsync(purchase));

    [HttpPut]
    public async Task<IActionResult> Update(PurchaseModel purchase) => Ok(await _service.UpdatePurchaseAsync(purchase));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeletePurchaseAsync(id);
        return ok ? NoContent() : NotFound();
    }

    // פרטי רוכשים לפי מתנה
    [HttpGet("purchasers-by-gift/{giftId:int}")]
    public async Task<IActionResult> PurchasersByGift(int giftId)
        => Ok(await _service.GetPurchasersByGiftAsync(giftId));

    // סל
    [HttpGet("cart/{userId:int}")]
    public async Task<IActionResult> GetCart(int userId)
        => Ok(await _service.GetUserCartAsync(userId));

    [HttpPost("checkout/{userId:int}")]
    public async Task<IActionResult> Checkout(int userId)
        => Ok(new { completedItems = await _service.CheckoutAsync(userId) });
}
