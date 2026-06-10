using server.DTOs;

namespace server.Services.Interfaces;

public interface ITransactionProducerService
{
    Task ProduceTransactionAsync(TransactionEventDto transactionEvent, CancellationToken cancellationToken = default);
}
