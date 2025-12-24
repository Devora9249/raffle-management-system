namespace server.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }   = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
        public bool IsActive { get; set; } = true;
        public List<PurchaseModel> Cart { get; set; } = new();
    }
    public enum RoleEnum
    {
        Admin, //0
        Donor, //1
        User   //2
    }
}