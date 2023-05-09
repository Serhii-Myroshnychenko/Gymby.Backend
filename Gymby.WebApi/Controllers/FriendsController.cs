using AutoMapper;
using Gymby.Application.Config;
using Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gymby.WebApi.Controllers;

public class FriendsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IOptions<AppConfig> _config;

    public FriendsController(IMapper mapper, IOptions<AppConfig> config) =>
        (_mapper, _config) = (mapper, config);

    [Authorize]
    [HttpGet("pending-friends")]
    public async Task<IActionResult> GetPendingFriendsList()
    {
        var query = new GetPendingFriendsListQuery(_config)
        {
            UserId = UserId.ToString(),
        };

        return Ok(await Mediator.Send(query));
    }
    [Authorize]
    [HttpGet("friends")]
    public async Task<IActionResult> GetMyFriendsList()
    {
        var query = new GetMyFriendsListQuery(_config)
        {
            UserId = UserId.ToString(),
        };

        return Ok(await Mediator.Send(query));
    }
    [Authorize]
    [HttpPost("invite")]
    public async Task<IActionResult> InviteFriend([FromBody]FriendDto request)
    {
        var command = new InviteFriendCommand()
        {
            UserId = UserId.ToString(),
            Username = request.Username
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("accept-request")]
    public async Task<IActionResult> AcceptFriendship([FromBody]FriendDto request)
    {
        var command = new AcceptFriendshipCommand()
        {
            UserId = UserId.ToString(),
            Username = request.Username,
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("reject-request")]
    public async Task<IActionResult> RejectFriendship([FromBody]FriendDto request)
    {
        var command = new RejectFriendshipCommand()
        {
            UserId = UserId.ToString(),
            Username = request.Username,
        };

        return Ok(await Mediator.Send(command));
    }
}
