using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;
using server.DTOs;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserCreateDto createDto)
    {
        try
        {
            var user = await _userService.AddUserAsync(createDto);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDto>> Update(int id, [FromBody] UserUpdateDto updateDto)
    {
        try
        {
            // כמו בקטגוריות – אפשר להתעלם מ-id ולהשתמש ב-dto.Id,
            // אבל עדיף לסנכרן כדי למנוע בלבול:
            updateDto.Id = id;

            var user = await _userService.UpdateUserAsync(updateDto);

            if (user == null)
                return NotFound(new { message = $"User with ID {id} not found." });

            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (!result)
            return NotFound(new { message = $"User with ID {id} not found." });

        return NoContent();
        //מה זה אומר? אין תוכן להחזיר, אבל הפעולה הצליחה
    }
}
