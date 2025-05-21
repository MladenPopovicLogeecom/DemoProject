namespace Service.Exceptions.UserExceptions;

public class UserWithUsernameDoesNotExistException(string username)
    : Exception("User with username: \"" + username + "\" does not exist!");