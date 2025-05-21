using Service.Contracts.Repository;
using Service.Entities;
using Service.Exceptions.UserExceptions;
using Service.Services.Interfaces;
using Service.Services.JWT;

namespace Service.Services.Implementation;

public class UserService(IUserRepository userRepository, JwtHelper jwtHelper) : IUserService
{
    public async Task<User> AuthenticateBasic(string username, string password)
    {
        User? user = await userRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new UserWithUsernameDoesNotExistException(username);
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new WrongPasswordException(username, password);
        }

        return user;
    }

    public async Task<User?> GetUserByToken(string token)
    {
        return await userRepository.GetUserByToken(token);
    }

    public async Task<string> GenerateToken(string username, string password)
    {
        User? user = await userRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new UserWithUsernameDoesNotExistException(username);
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new WrongPasswordException(username, password);
        }

        if (user.Token == null)
        {
            string token = jwtHelper.GenerateToken(user);
            user.Token = token;
            await userRepository.UpdateUser(user);
        }

        return user.Token;
    }
}