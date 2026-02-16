public class RaffleStateService : IRaffleStateService
{
    public RaffleStatus Status { get; private set; } = RaffleStatus.Open;

    public void FinishRaffle()
    {
        Status = RaffleStatus.Finished;
    }
}
