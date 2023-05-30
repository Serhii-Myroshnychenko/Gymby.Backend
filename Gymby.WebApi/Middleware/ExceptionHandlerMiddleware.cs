using FluentValidation;
using Gymby.Application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Gymby.WebApi.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (ex)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundEntityException notFoundEntityException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(notFoundEntityException.Message);
                break;
            case InvalidUsernameException invalidUsernameException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(invalidUsernameException.Message);
                break;
            case InviteFriendException inviteFriendException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(inviteFriendException.Message);
                break;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if(result == null)
        {
            result = JsonSerializer.Serialize(new {error = ex.Message});
        }

        return context.Response.WriteAsync(result);
    }
}
