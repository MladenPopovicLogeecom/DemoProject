using FluentMigrator;

namespace Migrations.Migrations.Category;

[Migration(4,"Delete New Column")]
public class DeleteNewColumn : Migration
{
    public override void Up()
    {   
        //Delete.Column("MyColumnXD").FromTable("Category");
    }
    
    public override void Down()
    {
        //Alter.Table("Categories").AddColumn("MyColumnXD").AsString(100).Nullable();   
    }
}