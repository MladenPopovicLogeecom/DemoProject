using System.Threading.Channels;
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
            options.UseNpgsql(configuration.GetConnectionString("DBConnection")));
        
        services.AddHostedService<Deleter>();
        services.AddSingleton(Channel.CreateUnbounded<string>());
        services.AddScoped<ICategoryRepository, CategoryRepositoryPostgre>();
    });

var app = builder.Build();

await app.RunAsync();