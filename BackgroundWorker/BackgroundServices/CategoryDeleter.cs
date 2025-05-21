using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contracts.Repository;

namespace BackgroundWorker.BackgroundServices;

public class CategoryDeleter(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("[Deleter] Hard deleting categories");

                using IServiceScope? scope = scopeFactory.CreateScope();
                ICategoryRepository? repository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();

                await repository.HardDeleteBeforeDate(DateTime.UtcNow, -5);
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}