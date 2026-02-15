using server.Models;
using server.Services.Interfaces;
using server.Repositories.Interfaces;
using server.DTOs;
using AutoMapper;

namespace server.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);

        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category {id} not found");

            return _mapper.Map<CategoryResponseDto>(category);

        }


        public async Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<CategoryModel>(dto);


            var created = await _categoryRepository.AddCategoryAsync(category);

            return _mapper.Map<CategoryResponseDto>(created);

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

            return _mapper.Map<CategoryResponseDto>(updated);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (await _categoryRepository.HasGiftsAsync(id))
                throw new InvalidOperationException($"Cannot delete Category {id} because it has associated gifts.");
            var ok = await _categoryRepository.DeleteCategoryAsync(id);
            if (!ok)
                throw new KeyNotFoundException($"Category {id} not found");
        }


    }
}