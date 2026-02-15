using AutoMapper;
using server.Models;
using server.DTOs;

namespace server.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Entity -> Response DTO
        CreateMap<CategoryModel, CategoryResponseDto>();

        // Create DTO -> Entity
        CreateMap<CategoryCreateDto, CategoryModel>();

        // Update DTO -> Entity (partial update)
        CreateMap<CategoryUpdateDto, CategoryModel>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
