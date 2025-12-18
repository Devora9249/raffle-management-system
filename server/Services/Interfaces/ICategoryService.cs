using server.Models;
using server.Models.Enums;
using server.DTOs;

namespace server.Services.Interfaces
{
    public interface ICategoryService
    {
        //CRUD
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> AddCategoryAsync(CategoryCreateDto category);
      Task<CategoryModel> UpdateCategoryAsync(int id, CategoryUpdateDto dto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}