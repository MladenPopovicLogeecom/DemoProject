using FluentMigrator;

namespace Migrations.Migrations.User;

[Migration(8,"Rename User Column")]
public class RenameColumn : Migration
{
    public override void Up()
    {
        Rename.Column("FirstName").OnTable("Users").To("Username");
    }

    public override void Down()
    {
        //Delete.Column("DeletedAt").FromTable("Categories");
    }
}