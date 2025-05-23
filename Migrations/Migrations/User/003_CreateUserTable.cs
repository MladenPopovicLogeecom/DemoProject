using FluentMigrator;

namespace Migrations.Migrations.User;

[Migration(3, "Create User Table")]
public class CreateUserTable : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Username").AsString().NotNullable()
            .WithColumn("LastName").AsString().NotNullable()
            .WithColumn("Role").AsString().NotNullable()
            .WithColumn("Token").AsString().Nullable()
            .WithColumn("Password").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}