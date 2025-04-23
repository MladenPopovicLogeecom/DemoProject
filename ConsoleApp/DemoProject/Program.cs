using WebApplication1.model;
using WebApplication1.model.entities;
using WebApplication1.Service;

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
    var categoryService = services.GetRequiredService<CategoryService>();
    var database = services.GetRequiredService<Database>();
    SeedDatabase(database);
}

app.Run();

void SeedDatabase(Database database)
{
    for (var i = 0; i < 3; i++)
    {
        //ID ce svima biti 000-000, da bih mogao lakse da demonstriram deleteWithId
        var cat = new Category("Title " + i, "Code " + i,
            "Description " + i,
            null);
        database.Categories.Add(cat);
    }
}