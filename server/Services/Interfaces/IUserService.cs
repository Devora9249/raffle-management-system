using server.DTOs.Users;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> AddUserAsync(UserCreateDto createDto);
        Task<UserResponseDto> UpdateUserAsync(UserUpdateDto updateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
