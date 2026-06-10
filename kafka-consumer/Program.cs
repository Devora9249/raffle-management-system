using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using server.Services.Options;
using KafkaConsumer;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<KafkaSettingsOptions>(hostContext.Configuration.GetSection("Kafka"));
        services.AddHostedService<KafkaConsumerWorker>();
    })
    .Build();

await host.RunAsync();
