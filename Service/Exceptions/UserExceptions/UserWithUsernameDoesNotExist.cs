namespace Service.Exceptions.UserExceptions;

public class UserWithUsernameDoesNotExist(string username)
    : Exception("User with username: \"" + username + "\" does not exist!");