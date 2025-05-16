using DataEF.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Service.Contracts.Repository;
using Service.Entities;

namespace DataEF.Repositories;

public class UserRepositoryPostgre(ApplicationDbContext context):IUserRepository

{
    public async Task<User?> GetUserByUsername(string username)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Username == username); 
    }
    
}