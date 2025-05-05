using API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using PresentationLayer.Repositories.Implementations;
using Service.Contracts.Repository;
using Service.Services.Implementation;
using Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepositoryInMemory>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddValidatorsFromAssemblyContaining<ValidatorCategory>();
builder.Services.AddFluentValidationAutoValidation();


var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ICategoryService categoryService = services.GetRequiredService<ICategoryService>();
    categoryService.SeedDatabase();
}

app.Run();