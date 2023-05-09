using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;

public class GetMyFriendsListQuery : IRequest<List<ProfileVm>>
{
    public string UserId { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; }

    public GetMyFriendsListQuery(IOptions<AppConfig> options)
    {
        Options = options;
    }
}
