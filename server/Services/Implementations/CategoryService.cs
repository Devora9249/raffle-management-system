
using server.Models;
using server.Services.Interfaces;
using server.Repositories.Interfaces;

namespace server.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<CategoryModel> AddCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
  