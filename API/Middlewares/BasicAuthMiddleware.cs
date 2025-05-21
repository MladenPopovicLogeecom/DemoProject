using System.Net;
using System.Text;
using Microsoft.Extensions.Primitives;
using Service.Exceptions.UserExceptions;
using Service.Services.Interfaces;

namespace API.Middlewares;

public class BasicAuthMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
        {
            string authHeaderValue = authHeader.ToString();
            if (authHeaderValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                string token = authHeaderValue.Substring("Basic ".Length).Trim();
                try
                {
                    byte[] credentialBytes = Convert.FromBase64String(token);
                    string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                    if (credentials.Length == 2)
                    {
                        string username = credentials[0];
                        string password = credentials[1];

                        try
                        {
                            await userService.AuthenticateBasic(username, password);
                            
                        }
                        catch (UserWithUsernameDoesNotExistException userWithUsernameExists)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsync(userWithUsernameExists.Message);
                        }
                        catch (WrongPasswordException wrongPasswordException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            await context.Response.WriteAsync(wrongPasswordException.Message);
                        }
                        await next(context);
                        return;
                        
                    }
                }
                catch (Exception exception)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync(exception.Message);
                    return;
                }
            }
        }

        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        await context.Response.WriteAsync("Basic Authentication:Unauthorized");
    }
}