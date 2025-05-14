using System.Text.Json.Serialization;
using API.Validators;
using DataEF.EntityFramework;
using DataEF.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Services.Implementation;
using Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


//Dependency Injection
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryPostgre>();

//Controllers
builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<ValidatorCategory>();
builder.Services.AddFluentValidationAutoValidation();


// PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyPostgreSQLConnection")));
// in AppSettings.json


var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ICategoryService categoryService = services.GetRequiredService<ICategoryService>();
    //categoryService.SeedDatabase();
}

app.Run();