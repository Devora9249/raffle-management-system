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

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category {id} not found");

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name =  category.Name
            };
        }


        public async Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto)
        {
            var category = new CategoryModel
            {
                Name = dto.Name
            };

            var created = await _categoryRepository.AddCategoryAsync(category);

            return new CategoryResponseDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }


public async Task<CategoryResponseDto> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
{
    var existing = await _categoryRepository.GetCategoryByIdAsync(id);
    if (existing == null)
        throw new KeyNotFoundException($"Category {id} not found");

    if (dto.Name != null)
        existing.Name = dto.Name;

    var updated = await _categoryRepository.UpdateCategoryAsync(existing);

    if (updated == null)
        throw new KeyNotFoundException($"Category {id} not found");

    return new CategoryResponseDto
    {
        Id = updated.Id,
        Name = updated.Name
    };
}

        public async Task DeleteCategoryAsync(int id)
        {
            var ok = await _categoryRepository.DeleteCategoryAsync(id);
            if (!ok)
                throw new KeyNotFoundException($"Category {id} not found");
        }

    }
}