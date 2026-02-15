using AutoMapper;
using server.DTOs.Donors;
using server.Models;

namespace server.Mappings;

public class DonorProfile : Profile
{
    public DonorProfile()
    {
        // UserModel -> DonorListItemDto
        CreateMap<UserModel, DonorListItemDto>();

        // UserModel -> DonorWithGiftsDto
        CreateMap<UserModel, DonorWithGiftsDto>()
            .ForMember(
                dest => dest.DonorId,
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Gifts,
                opt => opt.Ignore() // ימולא ידנית מה-GiftService
            );
    }
}
