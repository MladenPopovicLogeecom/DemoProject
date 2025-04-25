using Service.Service.Implementation;
using Service.Service.Interface;
using Domain.Model;
using Domain.Model.Entities;
using PresentationLayer.Repository.Implementation;
using PresentationLayer.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

//TODO
//Note the difference vs Singleton and Scoped!
//Scoped is created again for every http request!!
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryInMemory>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepositoryInMemory>();



builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //var database = services.GetRequiredService<DatabaseInMemory>();
    var categoryService = services.GetRequiredService<ICategoryService>();
    SeedDatabase(categoryService);
}

app.Run();

void SeedDatabase(ICategoryService categoryService)
{
    for (var i = 0; i < 3; i++)
    {
        var cat = new Category("Title " + i, "Code " + i,
            "Description " + i,
            null);
        categoryService.AddCategory(cat);
    }
}