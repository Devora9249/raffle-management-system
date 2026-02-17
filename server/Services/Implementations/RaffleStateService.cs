public class RaffleStateService : IRaffleStateService
{
    private readonly ILogger<RaffleStateService> _logger;
    public RaffleStateService(ILogger<RaffleStateService> logger)
    {
        _logger = logger;
    }
    public RaffleStatus Status { get; private set; } = RaffleStatus.Open;

    public Boolean isFinished()
    {
        _logger.LogInformation("Checking if raffle is finished. Current status: {Status}", Status);
        return Status == RaffleStatus.Finished;
    }
    public void FinishRaffle()
    {
        Status = RaffleStatus.Finished;
        _logger.LogInformation("Raffle finished. Current status: {Status}", Status);
    }

    public void Reset()
    {
        Status = RaffleStatus.Open;
        _logger.LogInformation("Raffle reset. Current status: {Status}", Status);
    }
}
 