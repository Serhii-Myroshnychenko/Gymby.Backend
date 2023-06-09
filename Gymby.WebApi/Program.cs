using Gymby.Application.Common.Mappings;
using Gymby.Application.Config;
using Gymby.Application.DI;
using Gymby.Application.Interfaces;
using Gymby.Persistence.Data;
using Gymby.Persistence.DI;
using Gymby.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppConfig>(configuration);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});
builder.Services.AddPersistence(configuration);
builder.Services.AddApplication(configuration["AzureBlobStorage"]!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.AllowAnyHeader();
        policy.WithOrigins("http://localhost:3000","https://gymby-web.azurewebsites.net");
        policy.AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://gymby-auth.azurewebsites.net";
    options.Audience = "GymbyWebAPI";
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception ex)
    {

    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddlewareHandler();

app.UseCors("Default");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}