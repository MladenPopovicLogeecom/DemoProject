using Service.Entities;

namespace Service.Services.Interfaces;

public interface IUserService
{
    Task<User> BasicAuthentication(string username, string password);
    Task<String> JwtAuthentication(string username, string password);
    Task<User?> GetUserByToken(string token);
    
}