namespace server.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public RoleEnum Role { get; set; }
        public List<PurchaseModel> Cart { get; set; }
    }

    public enum RoleEnum
    {
        Admin,
        Donor,
        User
    }
}