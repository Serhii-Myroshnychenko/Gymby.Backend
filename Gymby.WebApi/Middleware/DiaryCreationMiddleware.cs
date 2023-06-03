using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;
using MediatR;
using System.Security.Claims;

namespace Gymby.WebApi.Middleware;

public class DiaryCreationMiddleware
{
    private readonly RequestDelegate _next;

    public DiaryCreationMiddleware(RequestDelegate next) =>
        (_next) = (next);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var mediator = httpContext.RequestServices.GetService<IMediator>()!;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await mediator.Send(new CreateDiaryCommand() { UserId = userId });
            }
        }
        await _next(httpContext!);
    }
}
