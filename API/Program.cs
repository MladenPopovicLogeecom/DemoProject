using System.Threading.Channels;
using API.Validators;
using DataEF.EntityFramework;
using DataEF.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Service.BackgroundServices;
using Service.Contracts.Repository;
using Service.Services.Implementation;
using Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


//Dependency Injection
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryPostgre>();
//Background process
builder.Services.AddSingleton(Channel.CreateUnbounded<string>());
builder.Services.AddHostedService<LoggerBackgroundService>();


//Controllers
builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<ValidatorCategory>();
builder.Services.AddFluentValidationAutoValidation();

// PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));
// in AppSettings.json

var app = builder.Build();

app.MapControllers();


app.Run();