namespace server.Models
{
    public class WinningModel
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int WinnerId { get; set; }
        public UserModel User { get; set; }
        public GiftModel Gift { get; set; }

        // public int RaffleId { get; set; }
        // public RaffleModel Raffle { get; set; }

    }
}
