using server.Models;

namespace server.Models
{
    public class DonorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<GiftModel> Gifts { get; set; } = new();
    }
}