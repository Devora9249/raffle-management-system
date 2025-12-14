using server.Models;
using server.Models.Enums;

namespace server.Services.Interfaces
{
    public interface ICategoryService
    {
        //CRUD
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel> AddCategoryAsync(CategoryModel category);
        Task<CategoryModel> UpdateCategoryAsync(CategoryModel category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}