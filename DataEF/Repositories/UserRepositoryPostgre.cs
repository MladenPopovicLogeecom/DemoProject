using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class UserRepositoryPostgre(ApplicationDbContext context) : IUserRepository

{
    public async Task<User?> GetUserByUsername(string username)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateUser(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByToken(string token)
    {
        return await context.Users.FirstOrDefaultAsync(u=> u.Token == token);
    }
}