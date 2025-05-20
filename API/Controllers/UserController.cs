using API.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions.UserExceptions;
using Service.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("jwtLogin")]
    public async Task<ActionResult<string>> JwtLogin(LoginDto loginDto)
    {
        try
        {
            return Ok(await userService.JwtAuthentication(loginDto.Username, loginDto.Password));
        }
        catch (Exception ex) when (ex is UserWithUsernameDoesNotExist || ex is WrongPasswordException)
        {
            return Unauthorized("Invalid username or password");
        }
    }
    

}