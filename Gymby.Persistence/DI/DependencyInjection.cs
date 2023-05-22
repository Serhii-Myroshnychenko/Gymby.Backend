using Gymby.Application.Interfaces;
using Gymby.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gymby.Persistence.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext>(options =>
            options.GetService<ApplicationDbContext>()!);
        return services;
    }
}
