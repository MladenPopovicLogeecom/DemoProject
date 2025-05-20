using API.Middlewares;
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
using Service.Services.JWT;

var builder = WebApplication.CreateBuilder(args);


//Dependency Injection
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryPostgre>();
builder.Services.AddTransient<IUserRepository, UserRepositoryPostgre>();
builder.Services.AddSingleton<MessageChannel>();
builder.Services.AddTransient<JwtHelper>();

//Background process
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

//Security
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => { });

builder.Services.AddAuthorization();

var app = builder.Build();

//Middleware
//app.UseMiddleware<BasicAuthMiddleware>();
app.UseMiddleware<JwtAuthentificationMiddleware>();
app.UseMiddleware<JwtAuthorizationMiddleware>();

//Security
app.UseAuthorization();
app.UseAuthorization();


app.MapControllers();


app.Run();