using Azure.Storage.Blobs;
using FluentValidation;
using Gymby.Application.Common.Behaviors;
using Gymby.Application.Interfaces;
using Gymby.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gymby.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        string azureBlobStorage)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddScoped(_ =>
        {
            return new BlobServiceClient(azureBlobStorage);
        });

        services.AddScoped<IFileService, FileService>();
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehavior<,>));
        return services;
    }
}
