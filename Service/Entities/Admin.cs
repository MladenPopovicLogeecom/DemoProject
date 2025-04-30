namespace Service.Entities;

public class Admin(string firstName, string lastName, string password)
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    //Hashed!
    public required string Password { get; set; } = password;
}