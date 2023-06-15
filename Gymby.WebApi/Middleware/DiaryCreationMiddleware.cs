using Gymby.Application.Mediatr.Diaries.Command.CreateDiary;
using MediatR;
using System.Security.Claims;

namespace Gymby.WebApi.Middleware;

public class DiaryCreationMiddleware
{
    private readonly RequestDelegate _next;
    private static SemaphoreSlim _diaryCreationSemaphore = new(1);

    public DiaryCreationMiddleware(RequestDelegate next) =>
        (_next) = (next);

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var mediator = httpContext.RequestServices.GetService<IMediator>()!;
        await _diaryCreationSemaphore.WaitAsync();
        try
        {
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    await mediator.Send(new CreateDiaryCommand() { UserId = userId });
                }
            }
        }
        finally { _diaryCreationSemaphore.Release(); }
        
        await _next(httpContext!);
    }
}
