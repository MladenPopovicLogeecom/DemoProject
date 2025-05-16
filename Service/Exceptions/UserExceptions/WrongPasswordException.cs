namespace Service.Exceptions.UserExceptions;

public class WrongPasswordException(string username,string password) : 
    Exception("Password: "+password+" does not match username: "+username);