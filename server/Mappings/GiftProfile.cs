using AutoMapper;
using server.Models;
using server.DTOs;

namespace server.Mappings;

public class GiftProfile : Profile
{
    public GiftProfile()
    {
        // GiftModel -> GiftResponseDto
        CreateMap<GiftModel, GiftResponseDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name)
            );
    }
}
