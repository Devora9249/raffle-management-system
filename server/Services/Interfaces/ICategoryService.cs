using server.Models;
using server.Models.Enums;
using server.DTOs;

namespace server.Services.Interfaces
{
  public interface ICategoryService
  {
    //CRUD
    Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto> GetCategoryByIdAsync(int id);
    Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto category);
    Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryUpdateDto dto);

    Task DeleteCategoryAsync(int id);
  }
}