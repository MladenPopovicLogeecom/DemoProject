using BackgroundWorker.BackgroundServices;
using DataEF.EntityFramework;
using DataEF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.BackgroundServices;
using Service.Contracts.Repository;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DBConnection")));

        //Background processes
        services.AddHostedService<CategoryDeleter>();
        services.AddHostedService<RecentProductsUnflagger>();
        
        services.AddSingleton<MessageChannel>();
        services.AddTransient<ICategoryRepository, CategoryRepositoryPostgre>();
        services.AddTransient<IProductRepository, ProductRepositoryPostgre>();
    });

var app = builder.Build();

await app.RunAsync();