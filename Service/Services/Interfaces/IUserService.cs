namespace Service.Services.Interfaces;

public interface IUserService
{
    Task CheckCredentials(string username, string password);
}