using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileQuery : IRequest<ProfileVm>
{
    public string UserId { get; set; } = null!;
}
