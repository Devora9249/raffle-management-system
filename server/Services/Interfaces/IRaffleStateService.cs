public interface IRaffleStateService
{
    RaffleStatus Status { get; }
    Boolean isFinished();
    void FinishRaffle();
    void Reset();
}
