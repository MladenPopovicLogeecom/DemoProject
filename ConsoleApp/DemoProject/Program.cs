
using DemoProject.model;
using DemoProject.model.entities;
using DemoProject.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<CategoryService>();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var database = services.GetRequiredService<Database>();
    SeedDatabase(database);
}

app.Run();

void SeedDatabase(Database database)
{
    for (var i = 0; i < 3; i++)
    {
        var cat = new Category("Title " + i, "Code " + i,
            "Description " + i,
            null);
        database.Categories.Add(cat);
    }
}