using AutoMapper;
using server.Models;
using server.DTOs;

namespace server.Mappings;

public class WinningProfile : Profile
{
    public WinningProfile()
    {
        // Entity -> Response DTO
        CreateMap<WinningModel, WinningResponseDto>()
            .ForMember(
                dest => dest.giftName,
                opt => opt.MapFrom(src => src.Gift != null ? src.Gift.Description : "")
            )
            .ForMember(
                dest => dest.winnerName,
                opt => opt.MapFrom(src => src.User != null ? src.User.Name : "")
            );

        // Create DTO -> Entity
        CreateMap<WinningCreateDto, WinningModel>();
    }
}
