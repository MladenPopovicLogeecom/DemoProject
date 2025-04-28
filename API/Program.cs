using API.Validators;
using Service.Service.Implementation;
using Service.Service.Interface;
using Domain.Model.Entities;
using FluentValidation.AspNetCore;
using PresentationLayer.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();

//TODO : change when DI 
//builder.Services.AddSingleton<ICategoryRepository, CategoryRepositoryInMemory>();

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
    //TODO: Do like this when DI is introduced
    
    // var services = scope.ServiceProvider;
    // var categoryService = services.GetRequiredService<ICategoryService>();
    // categoryService.seedDatabase();

     //CategoryService s = new CategoryService();
    // for (var i = 0; i < 3; i++)
    // {
    //     var cat = new Category("Title " + i, "Code " + i,
    //         "Description " + i,
    //         null);
    //     //I did not use AddCategory, beacause it creates new Guid
    //     //For seeding database, i want to have "000-000...-000" Guid for every category, just for easier testing
    //         
    //     CategoryRepositoryInMemory.AddCategory(cat);
    //     //AddCategory(cat);
    // }
    //s.seedDatabase();
    

}

app.Run();

