using System.Net;
using Service.Services.Interfaces;
using Service.Services.JWT;

namespace API.Middlewares;

public class JwtAuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService, JwtHelper jwtHelper)
    {
        if (context.Request.Path.StartsWithSegments("/user/jwtLogin", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        var token = authHeader?.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) == true
            ? authHeader.Substring("Bearer ".Length).Trim()
            : null;

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("JWT Middleware: No token provided.");
            return;
        }

        var principal = jwtHelper.ValidateToken(token);
        if (principal == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("JWT Middleware: Invalid token.");
            return;
        }

        context.User = principal;

        await next(context);
    }
}