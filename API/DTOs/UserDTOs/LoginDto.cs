namespace API.DTOs.UserDTOs;

public class LoginDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }

}