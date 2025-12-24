using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;
using server.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    // [HttpGet("{id}")]
    // [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
    // {
    //     var category = await _categoryService.GetCategoryByIdAsync(id);

    //     if (category == null)
    //     {
    //         return NotFound(new { message = $"Category with ID {id} not found." });
    //     }

    //     return Ok(category);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }


    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create([FromBody] CategoryCreateDto dto)
    {
        var category = await _categoryService.AddCategoryAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = category.Id }, category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> Update(int id, [FromBody] CategoryUpdateDto dto)
    {
        var category = await _categoryService.UpdateCategoryAsync(id, dto);
        return Ok(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

}
