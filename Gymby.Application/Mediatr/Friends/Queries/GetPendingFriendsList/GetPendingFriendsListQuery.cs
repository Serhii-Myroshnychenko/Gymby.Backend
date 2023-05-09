using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using Microsoft.Extensions.Options;
using MediatR;

namespace Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;

public class GetPendingFriendsListQuery : IRequest<List<ProfileVm>>
{
    public string UserId { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; }

    public GetPendingFriendsListQuery(IOptions<AppConfig> options)
    {
        Options = options;
    }
}
