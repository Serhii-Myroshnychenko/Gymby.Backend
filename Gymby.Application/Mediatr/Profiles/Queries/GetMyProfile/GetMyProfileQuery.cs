using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileQuery : IRequest<ProfileVm>
{
    public IOptions<AppConfig> Options { get; set; }
    public string UserId { get; set; } = null!;

    public GetMyProfileQuery(IOptions<AppConfig> options)
    {
        Options = options;
    }
}
