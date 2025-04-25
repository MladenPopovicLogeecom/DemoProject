using API.Validators;
using Service.Service.Implementation;
using Service.Service.Interface;
using Domain.Model.Entities;
using FluentValidation.AspNetCore;
using PresentationLayer.Repository.Implementation;
using PresentationLayer.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

//TODO
//Note the difference vs Singleton and Scoped!
//Scoped is created again for every http request!!
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();

//TODO : change when DI 
builder.Services.AddSingleton<ICategoryRepository, CategoryRepositoryInMemory>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
    {
        cfg.RegisterValidatorsFromAssemblyContaining<ValidatorCategory>();
    });

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var categoryService = services.GetRequiredService<ICategoryService>();
    categoryService.seedDatabase();
    
}

app.Run();

