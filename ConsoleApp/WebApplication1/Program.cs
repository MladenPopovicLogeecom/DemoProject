using WebApplication1.model;
using WebApplication1.model.entities;

var builder = WebApplication.CreateBuilder(args);

//Registrujes Database kao singleton zbog Dependency Injection-a,
builder.Services.AddSingleton<Database>();

// Registruje podršku za controllere, ovo obavezno moras da navedes ako radis sa kontrolerima
builder.Services.AddControllers();

var app = builder.Build();

// Mapira controllere (kao da kažeš: "slušaj rute iz tih klasa")
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //Ova linija uzima Database iz dependency Injectiona, zato smo morali da je registrujemo.
    var database = services.GetRequiredService<Database>();
    SeedDatabase(database);
}

app.Run();

void SeedDatabase(Database database)
{
    for (var i = 0; i < 3; i++)
    {
        //ID ce svima biti 000-000, da bih mogao lakse da demonstriram deleteWithId
        Category cat = new Category("Title " + i.ToString(), "Code " + i.ToString(), 
            "Description " + i,
            null);
        database.AddCategory(cat);
    }
    
    
}

