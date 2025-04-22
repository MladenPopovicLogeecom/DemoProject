namespace WebApplication1.model.entities;

public class Admin
{
    //Pogledaj da li mogu drugacije da se pisu ove set metode, tj zasto se bas ovako pisu
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }

    public Admin(string firstName, string lastName, string password)
    {
        Id = Guid.NewGuid(); //On ce na nivou aplikacije da generise jedinstveni broj.
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}