using FluentMigrator;

namespace Migrations.Migrations.Category;

[Migration(3,"Add New Record")]
public class AddNewRecord  : Migration
{
    public override void Up()
    {
        // Insert.IntoTable("Categories").Row(new
        //     { Id = Guid.NewGuid(), Title = "Migration Desc", Description = "Migration Desc",
        //         Code = "8080880" });
    }
    
    public override void Down()
    {
        throw new NotImplementedException();
    }
}