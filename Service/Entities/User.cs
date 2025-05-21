using Service.Entities.Enums;

namespace Service.Entities;

public class User(string username, string lastName, string password, Role role)
{
    public Guid? Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; } = username;
    public string LastName { get; init; } = lastName;
    public Role Role { get; init; } = role;
    public string? Token { get; set; }

    //Hashed!
    public required string Password { get; init; } = password;
}