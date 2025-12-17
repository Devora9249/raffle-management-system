using System.ComponentModel.DataAnnotations;
using server.Models;

namespace server.DTOs
{
    public class UserCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public RoleEnum Role { get; set; } = RoleEnum.User;
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }

        public RoleEnum? Role { get; set; }
    }

    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }

        public static UserResponseDto FromModel(UserModel user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                City = user.City,
                Address = user.Address,
                Role = user.Role,

            };
        }
    }


}



