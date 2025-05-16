using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.CategoryExceptions;
using Service.Exceptions.UserExceptions;
using Service.Services.Interfaces;

namespace Service.Services.Implementation;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task CheckCredentials(string username,string password)
    {
        User? user = await userRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new UserWithUsernameDoesNotExist(username); 
        }
        
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new WrongPasswordException(username, password);
        }

        
    }
}