using server.DTOs.Donors;
using server.Models;

namespace server.Services.Interfaces
{
    public interface IDonorService
    {
        // Admin
        Task<IEnumerable<DonorListItemDto>> GetDonorsAsync(string? search, string? city);
        Task<IEnumerable<DonorWithGiftsDto>> GetDonorsWithGiftsAsync();
        Task SetUserRoleAsync(int userId, RoleEnum role);

        // Donor dashboard
        Task<DonorDashboardResponseDto> GetDonorDashboardAsync(int donorId);

        Task<DonorListItemDto?> GetDonorDetailsAsync(int userId);
        Task<addDonorDto> AddDonorAsync(addDonorDto donorDto);

    }
}
