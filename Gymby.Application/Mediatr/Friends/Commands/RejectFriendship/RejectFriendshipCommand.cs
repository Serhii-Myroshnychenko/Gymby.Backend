using MediatR;

namespace Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;

public class RejectFriendshipCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
}