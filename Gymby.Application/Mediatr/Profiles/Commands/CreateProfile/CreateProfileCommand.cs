using MediatR;

namespace Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

public class CreateProfileCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public string Email { get; set; } = null!;
}
