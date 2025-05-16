using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Service.Exceptions.CategoryExceptions;
using Service.Exceptions.UserExceptions;
using Service.Services.Interfaces;

namespace API.Middlewares;

public class BasicAuthMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var authHeaderValue = authHeader.ToString();
            if (authHeaderValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeaderValue.Substring("Basic ".Length).Trim();
                try
                {
                    var credentialBytes = Convert.FromBase64String(token);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                    if (credentials.Length == 2)
                    {
                        var username = credentials[0];
                        var password = credentials[1];

                        try
                        {
                            await userService.CheckCredentials(username, password);
                            
                        }
                        catch (UserWithUsernameDoesNotExist userwithUsernameExists)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsync("User with that username does not exist.");
                        }
                        catch (WrongPasswordException wrongPasswordException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsync("Wrong password.");
                        }
                        await next(context);
                        return;
                        
                    }
                }
                catch
                {
                    
                }
            }
        }

        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        await context.Response.WriteAsync("Unauthorized");
    }
}