using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace server.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;


    public AuthService(
        IUserRepository userRepository, 
        JwtService jwtService, 
        IConfiguration config,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _config = config;
        _logger = logger;
    }

    //  התחברות
    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

        try
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed: User with email {Email} not found", dto.Email);
                return null;
            }

            if (!VerifyPassword(dto.Password, user.Password))
            {
                _logger.LogWarning("Login failed: Incorrect password for email {Email}", dto.Email);
                return null;
            }

            var token = _jwtService.CreateToken(
                user.Id,
                user.Email,
                user.Role
            );

            _logger.LogInformation("User {Email} logged in successfully", dto.Email);

            return new LoginResponseDto
            {
                Token = token,
                ExpiresIn = int.Parse(
                            _config.GetSection("Jwt")["ExpiresMinutes"]!) * 60,
                User = UserResponseDto.FromModel(user)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login for email: {Email}", dto.Email);
            throw;
        }
    }

    //  הרשמה
    public async Task<LoginResponseDto> RegisterAsync(RegisterDto dto)
    {
        _logger.LogInformation("Attempting to register new user with email: {Email}", dto.Email);

        try
        {
            var exists = await _userRepository.GetByEmailAsync(dto.Email);
            if (exists != null)
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists", dto.Email);
                throw new ArgumentException("Email already exists");
            }

            var user = new UserModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                City = dto.City,
                Address = dto.Address,
                Password = HashPassword(dto.Password),
                Role = RoleEnum.User
            };

            await _userRepository.AddUserAsync(user);
            _logger.LogInformation("User {Email} successfully registered with ID: {UserId}", user.Email, user.Id);

            var token = _jwtService.CreateToken(
                user.Id,
                user.Email,
                user.Role
            );

            return new LoginResponseDto
            {
                Token = token,
                ExpiresIn = int.Parse(_config.GetSection("Jwt")["ExpiresMinutes"]!) * 60,

                User = UserResponseDto.FromModel(user)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during registration for email: {Email}", dto.Email);
            throw;
        }
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}