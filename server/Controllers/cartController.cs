using Microsoft.AspNetCore.Mvc;
using server.Dtos.Cart;
using server.Dtos.Purchases;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public CartController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    // הצגת סל של משתמש (Draft)
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<List<PurchaseReadDto>>> GetCart(int userId)
    {
        var items = await _purchaseService.GetUserCartAsync(userId);

        var dto = items.Select(p => new PurchaseReadDto
        {
            Id = p.Id,
            UserId = p.UserId,
            GiftId = p.GiftId,
            Qty = p.Qty,
            Status = p.Status,
            PurchaseDate = p.PurchaseDate
        }).ToList();

        return Ok(dto);
    }

    // הוספה לסל (תמיד Draft)
    [HttpPost("add")]
    public async Task<ActionResult<PurchaseReadDto>> AddToCart([FromBody] AddToCartDto dto)
    {
        if (dto.Qty <= 0)
            return BadRequest(new { message = "Qty must be greater than 0" });

        var purchase = new PurchaseModel
        {
            UserId = dto.UserId,
            GiftId = dto.GiftId,
            Qty = dto.Qty,
            Status = Status.Draft,
            PurchaseDate = DateTime.UtcNow
        };

        var created = await _purchaseService.AddAsync(purchase);

        return Ok(new PurchaseReadDto
        {
            Id = created.Id,
            UserId = created.UserId,
            GiftId = created.GiftId,
            Qty = created.Qty,
            Status = created.Status,
            PurchaseDate = created.PurchaseDate
        });
    }

    // עדכון כמות של פריט בסל (רק Draft)
    [HttpPut("{purchaseId:int}")]
    public async Task<IActionResult> UpdateQty(int purchaseId, [FromBody] UpdateCartQtyDto dto)
    {
        if (dto.Qty <= 0)
            return BadRequest(new { message = "Qty must be greater than 0" });

        var existing = await _purchaseService.GetByIdAsync(purchaseId);
        if (existing == null)
            return NotFound(new { message = "Cart item not found" });

        if (existing.Status != Status.Draft)
            return BadRequest(new { message = "Only Draft items can be updated in cart" });

        existing.Qty = dto.Qty;

        var updated = await _purchaseService.UpdateAsync(existing);
        return Ok(new PurchaseReadDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            GiftId = updated.GiftId,
            Qty = updated.Qty,
            Status = updated.Status,
            PurchaseDate = updated.PurchaseDate
        });
    }

    // מחיקת פריט מהסל (רק Draft)
    [HttpDelete("{purchaseId:int}")]
    public async Task<IActionResult> Remove(int purchaseId)
    {
        var existing = await _purchaseService.GetByIdAsync(purchaseId);
        if (existing == null)
            return NotFound(new { message = "Cart item not found" });

        if (existing.Status != Status.Draft)
            return BadRequest(new { message = "Only Draft items can be deleted from cart" });

        var ok = await _purchaseService.DeleteAsync(purchaseId);
        return ok ? NoContent() : NotFound();
    }

    // Checkout: כל Draft של המשתמש -> Completed
    [HttpPost("checkout/{userId:int}")]
    public async Task<ActionResult<CheckoutResultDto>> Checkout(int userId)
    {
        var completedCount = await _purchaseService.CheckoutAsync(userId);

        if (completedCount == 0)
            return BadRequest(new CheckoutResultDto
            {
                UserId = userId,
                ItemsCompleted = 0,
                Message = "Cart is empty"
            });

        return Ok(new CheckoutResultDto
        {
            UserId = userId,
            ItemsCompleted = completedCount,
            Message = "Checkout completed successfully"
        });
    }
}
