using Service.Entities;

namespace Service.Services.Interfaces;

public interface IUserService
{
    Task<User> BasicAuthentification(string username, string password);
    Task<String> JwtAuthentification(string username, string password);
    Task<User?> GetUserByToken(string token);
    
}