using System.Threading.Channels;
using Microsoft.Extensions.Hosting;

namespace Service.BackgroundServices;

public class LoggerBackgroundService(Channel<string> channel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await foreach (var message in channel.Reader.ReadAllAsync(stoppingToken))
            {
                Console.WriteLine($"[Logger] {message}");
            }
        }
    }
    
}