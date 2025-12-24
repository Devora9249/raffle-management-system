using server.Models;
using server.Models.Enums;

namespace server.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        //CRUD
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<CategoryModel?> GetCategoryByIdAsync(int id);
        Task<CategoryModel> AddCategoryAsync(CategoryModel category);
        Task<CategoryModel?> UpdateCategoryAsync(CategoryModel category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}