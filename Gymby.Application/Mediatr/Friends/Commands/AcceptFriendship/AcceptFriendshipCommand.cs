using MediatR;
namespace Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

public class AcceptFriendshipCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
}
