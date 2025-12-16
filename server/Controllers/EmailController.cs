using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WinningController : ControllerBase
    {
        private readonly IWinningService _winningService;

        public WinningController(IWinningService winningService)
        {
            _winningService = winningService;
        }

        // POST: /api/winning/send-mail?giftId=1&winnerId=3
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendWinningMail([FromQuery] int giftId, [FromQuery] int winnerId)
        {
            await _winningService.SendWinningEmailAsync(giftId, winnerId);
            return Ok("Mail sent");
        }
    }
}
