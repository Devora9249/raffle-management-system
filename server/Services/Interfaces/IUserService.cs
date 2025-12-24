using server.DTOs;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<UserResponseDto> AddUserAsync(UserCreateDto createDto);
        Task<UserResponseDto> UpdateUserAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
