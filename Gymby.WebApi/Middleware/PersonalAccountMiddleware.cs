using Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;
using MediatR;
using System.Security.Claims;

namespace Gymby.WebApi.Middleware;

public class PersonalAccountMiddleware
{
    private readonly RequestDelegate _next;

    public PersonalAccountMiddleware(RequestDelegate next) =>
        (_next) = (next);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var mediator = httpContext.RequestServices.GetService<IMediator>()!;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = httpContext.User.FindFirst(ClaimTypes.Email)!.Value;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(email))
            {
                await mediator.Send(new CreateProfileCommand() { UserId = userId, Email = email });
            }
        }
        await _next(httpContext!);
    }
}
