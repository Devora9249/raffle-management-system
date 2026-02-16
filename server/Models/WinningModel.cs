namespace server.Models
{
    public class WinningModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int WinnerId { get; set; }
        public UserModel User { get; set; } = default!;
        public GiftModel Gift { get; set; }=default!;

     

    }
}
 