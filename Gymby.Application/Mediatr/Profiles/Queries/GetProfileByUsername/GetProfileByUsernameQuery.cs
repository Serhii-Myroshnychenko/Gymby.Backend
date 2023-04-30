using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

public class GetProfileByUsernameQuery : IRequest<ProfileVm>
{
    public string Username { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; }

    public GetProfileByUsernameQuery(IOptions<AppConfig> options)
    {
        Options = options;
    }
}
