using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contracts.Repository;

namespace BackgroundWorker.BackgroundServices;

public class RecentlyVisitedProductsCleaner(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromSeconds(10));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("[ProductVisitedAtCleaner] Unflagging recent products.");

                using var scope = scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                await repository.CleanRecentlyVisitedProducts();
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}