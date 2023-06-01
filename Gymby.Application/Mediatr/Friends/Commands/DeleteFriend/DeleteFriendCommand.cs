using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Friends.Commands.DeleteFriend;

public class DeleteFriendCommand : IRequest<List<ProfileVm>>
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; } = null!;
}