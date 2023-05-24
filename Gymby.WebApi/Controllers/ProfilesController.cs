using Gymby.Application.Config;
using Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;
using Gymby.Application.ViewModels;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gymby.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : BaseController
{
    private readonly IOptions<AppConfig> _config;

    public ProfilesController(IOptions<AppConfig> config) =>
        (_config) = (config);

    [Authorize]
    [HttpGet("{username}")]
    public async Task<ActionResult<ProfileVm>> GetProfileByUsername(string username)
    {
        var query = new GetProfileByUsernameQuery(_config)
        {
            Username = username
        };

        return Ok(await Mediator.Send(query));
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ProfileVm>> GetMyProfile()
    {
        var query = new GetMyProfileQuery(_config)
        {
            UserId = UserId.ToString(),
            Email = Email,
        };

        return Ok(await Mediator.Send(query));
    }
    [Authorize]
    [HttpPost("update")]
    public async Task<ActionResult<ProfileVm>> UpdateProfile([FromForm] UpdateProfileDto updateProfile)
    {
        var command = new UpdateProfileCommand(_config) { ProfileId = updateProfile.ProfileId, UserId = UserId.ToString(), Username = updateProfile.Username, Email = updateProfile.Email, FirstName = updateProfile.FirstName, LastName = updateProfile.LastName, Description = updateProfile.Description, Avatar = updateProfile.Avatar, InstagramUrl = updateProfile.InstagramUrl, FacebookUrl = updateProfile.FacebookUrl, TelegramUsername = updateProfile.TelegramUsername };

        return Ok(await Mediator.Send(command));
    }
}
