using System.Net;
using System.Security.Claims;
using Service.Entities.Enums;
using Service.Services.Interfaces;

namespace API.Middlewares;

public class JwtAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (IgnoreSomeRoutes(context))
        {
            await next(context);
            return;
        }

        var roleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (!string.Equals(roleClaim, Role.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("JWT Middleware: You don't have admin role :((");
            return;
        }

        await next(context);
    }

    //All GET routes are allowed for everyone, we don't need auth check
    private bool IgnoreSomeRoutes(HttpContext context)
    {
        var path = context.Request.Path.Value?.TrimEnd('/') ?? "";
        var method = context.Request.Method;

        if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
            if (path.Equals("/category", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/category/parents", StringComparison.OrdinalIgnoreCase))
                return true;

            // GET /category/{guid}
            if (path.StartsWith("/category/", StringComparison.OrdinalIgnoreCase))
            {
                var remainder = path["/category/".Length..];
                if (!remainder.Contains("/") && Guid.TryParse(remainder, out _))
                    return true;
            }
        }

        if (path.Equals("/user/jwtLogin", StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }
}