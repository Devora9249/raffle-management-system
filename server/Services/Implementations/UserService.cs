using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();
            return users.Select(ToResponseDto);
        }

        public async Task<UserResponseDto> AddUserAsync(UserCreateDto createDto)
        {
            // בדיקות בסיסיות (אופציונלי)
            if (string.IsNullOrWhiteSpace(createDto.Name))
                throw new ArgumentException("Name is required");

            var user = new UserModel
            {
                Name = createDto.Name,
                Email = createDto.Email,
                Phone = createDto.Phone,
                City = createDto.City,
                Address = createDto.Address,
                Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password),
                Role = createDto.Role
            };

            var created = await _repo.AddUserAsync(user);
            return ToResponseDto(created);
        }

        public async Task<UserResponseDto> UpdateUserAsync(int id, UserUpdateDto dto)

        {

            var existing = await _repo.GetUserByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("User not found");

            if (dto.Name != null) existing.Name = dto.Name;
            if (dto.Email != null) existing.Email = dto.Email;
            if (dto.Phone != null) existing.Phone = dto.Phone;
            if (dto.City != null) existing.City = dto.City;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.Password != null) existing.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            if (dto.Role.HasValue) existing.Role = dto.Role.Value;

            var updated = await _repo.UpdateUserAsync(existing);
            return ToResponseDto(updated);
        }

        public Task<bool> DeleteUserAsync(int id)
            => _repo.DeleteUserAsync(id);

//מה הפונקציה הזו עושה?
        private static UserResponseDto ToResponseDto(UserModel user)
            => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                City = user.City,
                Address = user.Address,
                Role = user.Role
            };
    }
}
