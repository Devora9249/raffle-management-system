using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftController : ControllerBase
{
    private readonly IGiftService _service;

    public GiftController(IGiftService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllGiftsAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var gift = await _service.GetGiftByIdAsync(id);
        return gift == null ? NotFound() : Ok(gift);
    }

    [HttpPost]
    public async Task<IActionResult> Add(GiftModel gift) => Ok(await _service.AddGiftAsync(gift));

    [HttpPut]
    public async Task<IActionResult> Update(GiftModel gift) => Ok(await _service.UpdateGiftAsync(gift));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteGiftAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
        => Ok(await _service.SearchByNameAsync(name));

    [HttpGet("by-donor/{donorId:int}")]
    public async Task<IActionResult> ByDonor(int donorId)
        => Ok(await _service.FilterByDonorAsync(donorId));

    [HttpGet("sort")]
    public async Task<IActionResult> Sort([FromQuery] bool ascending = true)
        => Ok(await _service.SortByPriceAsync(ascending));
}
