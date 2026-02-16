using AutoMapper;
using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> AddUserAsync(UserCreateDto createDto)
        {
            // בדיקות בסיסיות 
            if (string.IsNullOrWhiteSpace(createDto.Name))
                throw new ArgumentException("Name is required");

            var user = _mapper.Map<UserModel>(createDto);


            var created = await _repo.AddUserAsync(user);
            return _mapper.Map<UserResponseDto>(created);
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
            return _mapper.Map<UserResponseDto>(updated);
        }

        public Task<bool> DeleteUserAsync(int id)
            => _repo.DeleteUserAsync(id);


    }
}
