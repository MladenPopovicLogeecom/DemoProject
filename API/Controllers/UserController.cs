using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{

    [HttpGet("login")]
    public void Login()
    {
        
    }
}