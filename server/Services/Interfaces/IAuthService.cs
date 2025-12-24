using server.DTOs;


namespace server.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDto?> LoginAsync(LoginDto dto);
        public Task<LoginResponseDto> RegisterAsync(RegisterDto dto);
        public string HashPassword(string password);


    }
}