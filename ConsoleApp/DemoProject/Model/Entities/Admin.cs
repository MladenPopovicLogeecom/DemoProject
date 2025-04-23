namespace WebApplication1.model.entities;

public class Admin
{
    public Admin(string firstName, string lastName, string password)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}