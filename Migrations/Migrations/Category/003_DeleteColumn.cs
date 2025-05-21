using FluentMigrator;

namespace Migrations.Migrations.Category;

[Migration(6,"Delete Column")]
public class DeleteColumn : Migration
{
    public override void Up()
    {
        //Delete.Column("DeletedAt").FromTable("Categories");  
    }

    public override void Down()
    {
        
    }

    
}