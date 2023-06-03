﻿namespace Gymby.WebApi.Middleware;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddlewareHandler(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionHandlerMiddleware>();
        return applicationBuilder;
    }
}
