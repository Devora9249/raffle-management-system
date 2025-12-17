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

        public async Task<UserResponseDto> UpdateUserAsync(UserUpdateDto updateDto)
        {
            var existing = await _repo.GetUserByIdAsync(updateDto.Id);
            if (existing == null)
                throw new KeyNotFoundException("User not found");

            if (updateDto.Name != null) existing.Name = updateDto.Name;
            if (updateDto.Email != null) existing.Email = updateDto.Email;
            if (updateDto.Phone != null) existing.Phone = updateDto.Phone;
            if (updateDto.City != null) existing.City = updateDto.City;
            if (updateDto.Address != null) existing.Address = updateDto.Address;
            if (updateDto.Password != null) existing.Password = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);
            if (updateDto.Role.HasValue) existing.Role = updateDto.Role.Value;

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
