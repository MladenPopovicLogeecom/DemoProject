using BackgroundWorker.BackgroundServices;
using DataEF.EntityFramework;
using DataEF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contracts.Repository;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddHostedService<Deleter>();
        services.AddScoped<ICategoryRepository, CategoryRepositoryPostgre>();
    });

var app = builder.Build();

await app.RunAsync();