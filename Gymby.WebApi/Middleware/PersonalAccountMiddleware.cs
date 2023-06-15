using Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gymby.WebApi.Middleware;

public class PersonalAccountMiddleware
{
    private readonly RequestDelegate _next;
    private static SemaphoreSlim _profileCreationSemaphore = new (1);

    public PersonalAccountMiddleware(RequestDelegate next) =>
        (_next) = (next);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var mediator = httpContext.RequestServices.GetService<IMediator>()!;
        await _profileCreationSemaphore.WaitAsync();
        try
        {
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = httpContext.User.FindFirst("name")!.Value;

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(email))
                {
                    await mediator.Send(new CreateProfileCommand() { UserId = userId, Email = email });
                }
            }
        }
        finally { _profileCreationSemaphore.Release(); }
        
        await _next(httpContext!);
    }
}
