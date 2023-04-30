using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<ProfileVm>
{
    public string ProfileId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? Username { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public IFormFile? Avatar { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TelegramUsername { get; set; }
    public IOptions<AppConfig> Options { get; set; }

    public UpdateProfileCommand(IOptions<AppConfig> options)
    {
        Options = options;
    }
}
