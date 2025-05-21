using FluentMigrator;

namespace Migrations.Migrations.Category;

[Migration(7,"Add New Column")]
public class AddNewColumn : Migration
{
    public override void Up()
    {
        //Alter.Table("Categories").AddColumn("DeletedAt").AsDateTime2().Nullable();   
    }

    public override void Down()
    {
        //Delete.Column("DeletedAt").FromTable("Categories");
    }
}