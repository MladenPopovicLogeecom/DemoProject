using Microsoft.Extensions.Hosting;

namespace BackgroundWorker.BackgroundServices;

public class Deleter : BackgroundService
{
    //PeriodicTimer timer= new PeriodicTimer(TimeSpan.FromMinutes(60));
    private readonly PeriodicTimer timer= new PeriodicTimer(TimeSpan.FromSeconds(2));
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine("Deleting Category");
            }
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}