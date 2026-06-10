using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using server.Services.Options;

namespace KafkaConsumer;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly KafkaSettingsOptions _settings;
    private readonly ILogger<KafkaConsumerWorker> _logger;

    public KafkaConsumerWorker(IOptions<KafkaSettingsOptions> options, ILogger<KafkaConsumerWorker> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);
    }

    private void ConsumeMessages(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _settings.BootstrapServers,
            GroupId = "raffle-transaction-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_settings.Topic);

        _logger.LogInformation("Kafka consumer subscribed to topic {Topic} using bootstrap servers {BootstrapServers}", _settings.Topic, _settings.BootstrapServers);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    if (consumeResult?.Message != null)
                    {
                        _logger.LogInformation("Consumed message from Kafka topic {Topic}: {Value}", _settings.Topic, consumeResult.Message.Value);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Kafka consume error");
                }
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Kafka consumer shutdown requested.");
        }
        finally
        {
            consumer.Close();
        }
    }
}
