using FluentMigrator;

namespace Migrations.Migrations.User;

[Migration(4, "Add users")]
public class AddUsers : Migration
{
    public override void Up()
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword("123");

        Insert.IntoTable("Users").Row(new
        {
            Id=Guid.NewGuid(),
            Username = "admin",
            LastName = "admin",
            Token=(string?)null,
            Password = passwordHash,
            Role = "Admin"
        });

        Insert.IntoTable("Users").Row(new
        {
            Id=Guid.NewGuid(),
            Username = "user",
            LastName = "user",
            Token=(string?)null,
            Password = passwordHash,
            Role = "User"
        });
        
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}