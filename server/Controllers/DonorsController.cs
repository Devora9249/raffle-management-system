using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _service;

    public DonorController(IDonorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllDonorsAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var donor = await _service.GetDonorByIdAsync(id);
        return donor == null ? NotFound() : Ok(donor);
    }

    [HttpPost]
    public async Task<IActionResult> Add(DonorModel donor) => Ok(await _service.AddDonorAsync(donor));

    [HttpPut]
    public async Task<IActionResult> Update(DonorModel donor) => Ok(await _service.UpdateDonorAsync(donor));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteDonorAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
