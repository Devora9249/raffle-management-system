using Confluent.Kafka;
using Microsoft.Extensions.Options;
using server.DTOs;
using server.Services.Interfaces;
using server.Services.Options;
using System.Text.Json;

namespace server.Services.Implementations;

public class KafkaTransactionProducerService : ITransactionProducerService, IDisposable
{
    private readonly IProducer<Null, string> _producer;
    private readonly KafkaSettingsOptions _settings;
    private readonly ILogger<KafkaTransactionProducerService> _logger;

    public KafkaTransactionProducerService(
        IOptions<KafkaSettingsOptions> kafkaSettings,
        ILogger<KafkaTransactionProducerService> logger)
    {
        _settings = kafkaSettings.Value;
        _logger = logger;

        var config = new ProducerConfig
        {
            BootstrapServers = _settings.BootstrapServers,
            Acks = Acks.All,
            EnableIdempotence = true,
            MessageTimeoutMs = 30000
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceTransactionAsync(TransactionEventDto transactionEvent, CancellationToken cancellationToken = default)
    {
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(transactionEvent)
        };

        var deliveryResult = await _producer
            .ProduceAsync(_settings.Topic, message, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation(
            "Produced Kafka event {EventType} to {TopicPartitionOffset}",
            transactionEvent.EventType,
            deliveryResult.TopicPartitionOffset);
    }

    public void Dispose()
    {
        try
        {
            _producer.Flush(TimeSpan.FromSeconds(10));
        }
        catch
        {
            // Swallow exceptions during shutdown flush.
        }

        _producer.Dispose();
    }
}
