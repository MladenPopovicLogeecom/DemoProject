using Microsoft.Extensions.Hosting;

namespace Service.BackgroundServices;

public class LoggerBackgroundService(MessageChannel messageChannel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
            await foreach (var message in messageChannel.Reader.ReadAllAsync(stoppingToken))
                Console.WriteLine($"[Logger] {message}");
    }
}