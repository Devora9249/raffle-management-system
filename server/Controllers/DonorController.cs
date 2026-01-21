using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;

        public DonorController(IDonorService donorService, IUserService userService)
        {
            _donorService = donorService;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<DonorListItemDto>>> GetDonors(
                            [FromQuery] string? search,
                            [FromQuery] string? city)
                            => Ok(await _donorService.GetDonorsAsync(search, city));

        [Authorize(Roles = "Admin")]
        [HttpPatch("role/{userId}")]
        public async Task<IActionResult> SetRole(int userId, [FromQuery] RoleEnum role)
        {
            await _donorService.SetUserRoleAsync(userId, role);
            return NoContent();
        }

        [Authorize(Roles = "Donor")]
        [HttpGet("dashboard")]
        public async Task<ActionResult<DonorDashboardResponseDto>> GetDonorDashboard()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            return await _donorService.GetDonorDashboardAsync(userId);
        }
        

        [Authorize(Roles = "Donor")]
        [HttpGet("details")]
        public async Task<ActionResult<DonorListItemDto?>> GetDonorDetails()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim);

            return await _donorService.GetDonorDetailsAsync(userId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddDonor([FromBody] addDonorDto donorDto)
        {
            await _donorService.AddDonorAsync(donorDto);
            return Ok(donorDto);

        }
    }
}