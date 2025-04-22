using Microsoft.AspNetCore.Mvc;
using WebApplication1.model.entities;

namespace WebApplication1.controller;

[ApiController]
[Route("product")]
public class ProductController : ControllerBase
{

    [HttpGet("test")]
    public void test()
    {
        Console.WriteLine("radi sve :D");
        
    }
    
}