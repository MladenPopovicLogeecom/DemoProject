using Service.Entities.Enums;

namespace Service.Entities;

public class User(string username, string lastName, string password,Role role)
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = username;
    public string LastName { get; set; } = lastName;
    public Role Role { get; set; } = role;
    public String? Token { get; set; }

    //Hashed!
    public required string Password { get; set; } = password;
    
}