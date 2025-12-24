using server.DTOs;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;




namespace server.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtService _jwtService;
    private readonly IConfiguration _config;


    public AuthService(IUserRepository userRepository, JwtService jwtService, IConfiguration config
)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _config = config;
    }

    //  התחברות
    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            return null;

        if (!VerifyPassword(dto.Password, user.Password))
            return null;

        var token = _jwtService.CreateToken(
            user.Id,
            user.Email,
            user.Role
        );

        return new LoginResponseDto
        {
            Token = token,
            ExpiresIn = int.Parse(
                        _config.GetSection("Jwt")["ExpiresMinutes"]!) * 60,
            User = UserResponseDto.FromModel(user)
        };

    }

    //  הרשמה
    public async Task<LoginResponseDto> RegisterAsync(RegisterDto dto)
    {
        var exists = await _userRepository.GetByEmailAsync(dto.Email);
        if (exists != null)
            throw new ArgumentException("Email already exists");

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

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }


}
