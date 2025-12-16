using Microsoft.AspNetCore.Mvc;
using server.DTOs.Donors;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        // -------- פעולות אדמין --------

        // GET /api/donor?search=...&city=...
        [HttpGet]
        public async Task<ActionResult<List<DonorListItemDto>>> GetDonors(
            [FromQuery] string? search,
            [FromQuery] string? city)
            => Ok(await _donorService.GetDonorsAsync(search, city));

        // PATCH /api/donor/role/5?role=Donor
        [HttpPatch("role/{userId}")]
        public async Task<IActionResult> SetRole(int userId, [FromQuery] RoleEnum role)
        {
            await _donorService.SetUserRoleAsync(userId, role);
            return Ok();
        }

        // -------- דשבורד לתורם --------

        // GET /api/donor/5/dashboard
        [HttpGet("{donorId}/dashboard")]
        public async Task<ActionResult<DonorDashboardResponseDto>> Dashboard(int donorId)
            => Ok(await _donorService.GetDonorDashboardAsync(donorId));
    }
}
