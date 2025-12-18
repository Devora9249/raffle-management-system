using server.Models;
using server.Services.Interfaces;
using server.Repositories.Interfaces;
using server.DTOs;

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

        public async Task<CategoryModel> AddCategoryAsync(CategoryCreateDto dto)
        {
            var category = new CategoryModel
            {
                Name = dto.Name
            };

            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            var category = new CategoryModel
            {
                Id = id,
                Name = dto.Name
            };

            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
