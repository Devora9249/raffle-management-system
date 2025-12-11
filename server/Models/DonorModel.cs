using server.Models;

namespace server.Models
{
    public class DonorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public List<GiftModel> Gifts { get; set; }
    }
}