using AutoMapper;
using server.Models;
using server.DTOs;

namespace server.Mappings;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        // Entity -> Response DTO
        CreateMap<PurchaseModel, PurchaseResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.GiftName, opt => opt.MapFrom(src => src.Gift.Description))
            .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.Gift.DonorId))
            .ForMember(dest => dest.DonorName, opt => opt.MapFrom(src => src.Gift.Donor.Name));

        // Create DTO -> Entity
        CreateMap<PurchaseCreateDto, PurchaseModel>();

        // Update DTO -> Entity (partial update)
        CreateMap<PurchaseUpdateDto, PurchaseModel>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
