using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

public class InviteFriendCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
}
