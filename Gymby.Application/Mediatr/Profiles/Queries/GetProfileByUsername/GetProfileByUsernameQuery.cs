using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

public class GetProfileByUsernameQuery : IRequest<ProfileVm>
{
    public string Username { get; set; } = null!;
}
