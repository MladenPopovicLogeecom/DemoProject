using Service.Entities;

namespace Service.Services.Interfaces;

public interface IUserService
{
    Task<User> AuthenticateBasic(string username, string password);
    Task<string> GenerateToken(string username, string password);
    Task<User?> GetUserByToken(string token);
    
}