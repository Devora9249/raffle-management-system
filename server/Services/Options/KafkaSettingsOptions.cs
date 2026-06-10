namespace server.Services.Options;

public class KafkaSettingsOptions
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
}
