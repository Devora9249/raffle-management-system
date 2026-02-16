using AutoMapper;
using server.DTOs;
using server.Models;

namespace server.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Entity -> Response DTO
        CreateMap<UserModel, UserResponseDto>();

        // Create DTO -> Entity
        CreateMap<UserCreateDto, UserModel>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        // Update DTO -> Entity (partial update)
        CreateMap<UserUpdateDto, UserModel>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
