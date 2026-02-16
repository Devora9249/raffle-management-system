public interface IRaffleStateService
{
    RaffleStatus Status { get; }
    void FinishRaffle();
}
