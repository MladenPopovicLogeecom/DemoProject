namespace PresentationLayer.Entities;

public class Admin(string firstName, string lastName, string password)
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public required string FirstName { get; set; } = firstName;
    public required string LastName { get; set; } = lastName;

    //Hashed!
    public required string Password { get; set; } = password;
}