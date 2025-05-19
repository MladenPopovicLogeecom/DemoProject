using Microsoft.Extensions.Hosting;
using Service.Contracts.Repository;

namespace BackgroundWorker.BackgroundServices;

public class Deleter(ICategoryRepository repository) : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromSeconds(60));
    private readonly ICategoryRepository repository = repository;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    Console.WriteLine("[Deleter] Hard deleting categories");
                    await repository.HardDeleteBeforeDate(DateTime.UtcNow, -5);
                }
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}