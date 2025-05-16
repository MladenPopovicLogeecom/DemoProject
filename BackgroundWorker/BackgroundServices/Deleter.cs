using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Hosting;
using Service.Contracts.Repository;
using Service.Services.Interfaces;

namespace BackgroundWorker.BackgroundServices;

public class Deleter(ICategoryRepository repository) : BackgroundService
{
    //PeriodicTimer timer= new PeriodicTimer(TimeSpan.FromMinutes(60));
    private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
    private readonly ICategoryRepository repository = repository;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                //Remember, UtcNow is safer, check that out and note it.
                Console.WriteLine("[Deleter] Hard deleting categories");
                await repository.HardDeleteBeforeDate(DateTime.UtcNow);
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}