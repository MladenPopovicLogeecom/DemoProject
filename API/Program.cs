using API.Middlewares;
using API.Validators;
using DataEF.EntityFramework;
using DataEF.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Service.BackgroundServices;
using Service.BusinessValidators;
using Service.Contracts.Repository;
using Service.Services.Implementation;
using Service.Services.Interfaces;
using Service.Services.JWT;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryPostgre>();
builder.Services.AddTransient<IUserRepository, UserRepositoryPostgre>();
builder.Services.AddTransient<IProductRepository, ProductRepositoryPostgre>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<CategoryBusinessValidator>();
builder.Services.AddTransient<ProductBusinessValidator>();

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

//Security
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", _ => { });

builder.Services.AddAuthorization();

WebApplication? app = builder.Build();

//Middleware
//app.UseMiddleware<BasicAuthMiddleware>();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseMiddleware<JwtAuthorizationMiddleware>();

//Security
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();