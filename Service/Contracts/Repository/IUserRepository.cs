using Service.Entities;

namespace Service.Contracts.Repository;

public interface IUserRepository
{
    Task<User?> GetUserByUsername(string username);
    Task UpdateUser(User user);
    Task<User?> GetUserByToken(string token);
}