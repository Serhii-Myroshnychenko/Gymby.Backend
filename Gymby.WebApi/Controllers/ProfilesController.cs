using Gymby.Application.Config;
using Gymby.Application.Mediatr.Friends.Queries.QueryFriends;
using Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;
using Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;
using Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;
using Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;
using Gymby.Application.ViewModels;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json;
using System.Web;

namespace Gymby.WebApi.Controllers;

[Route("api/profile")]
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
        var command = new UpdateProfileCommand(_config) { ProfileId = updateProfile.ProfileId, UserId = UserId.ToString(), Username = updateProfile.Username, Email = updateProfile.Email, FirstName = updateProfile.FirstName, LastName = updateProfile.LastName, Description = updateProfile.Description, PhotoAvatarPath = updateProfile.PhotoAvatarPath, InstagramUrl = updateProfile.InstagramUrl, FacebookUrl = updateProfile.FacebookUrl, TelegramUsername = updateProfile.TelegramUsername };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> QueryProfile(string? type, string? query)
    {
        var search = new QueryProfileQuery()
        {
            UserId = UserId.ToString(),
            Type = type,
            Query = query,
            Options = _config
        };

        return Ok(await Mediator.Send(search));

    }

    [Authorize]
    [HttpGet("friends/search")]
    public async Task<IActionResult> QueryFriend(string? type, string? query)
    {
        var search = new QueryFriendQuery()
        {
            UserId = UserId.ToString(),
            Type = type,
            Query = query,
            Options = _config
        };

        return Ok(await Mediator.Send(search));
    }


    [HttpPost("aaa")]
    public async Task<IActionResult> HandleResponse()
    {
        //var response = JsonConvert.DeserializeObject<ResponseModel>(json);

        //string requestBody = string.Empty;

        //using (var reader = new StreamReader(Request.Body))
        //{
        //    requestBody = await reader.ReadToEndAsync();
        //}

        //// Обработка данных от LiqPay
        //dynamic data = JsonConvert.DeserializeObject<ResponseModel>(requestBody);
        //// Извлечение нужной информации и выполнение необходимых действий
        //// ...

        //// Возвращение ответа серверу LiqPay
        //var response = new
        //{
        //    status = "ok"
        //};

        //return Ok(response);
        //// Обработка полученного объекта response
        ///

        string requestBody = string.Empty;

        using (var reader = new StreamReader(Request.Body))
        {
            requestBody = await reader.ReadToEndAsync();
        }

        // Deserialize the JSON string
        //var data = JsonConvert.DeserializeObject<ResponseModel>(requestBody);

        // Access the deserialized properties
        //var signature = data.Signature;


        var keyValuePairs = HttpUtility.UrlDecode(requestBody).Split('&');

        // Create a dictionary to store the key-value pairs
        var dataDictionary = new Dictionary<string, string>();

        // Parse each key-value pair and add it to the dictionary
        foreach (var pair in keyValuePairs)
        {
            var keyValue = pair.Split('=');
            if (keyValue.Length == 2)
            {
                var key = keyValue[0];
                var value = keyValue[1];

                if (dataDictionary.ContainsKey(key))
                {
                    // Key already exists, update the value
                    dataDictionary[key] = value;
                }
                else
                {
                    // Key doesn't exist, add it to the dictionary
                    dataDictionary.Add(key, value);
                }
            }
        }

        // Deserialize the dictionary into the ResponseModel
        var responseModel = new ResponseModel
        {
            Signature = dataDictionary["signature"],
            Data = dataDictionary["data"]
        };

        // Access the deserialized properties
        var signature = responseModel.Signature;
        var jsonData = responseModel.Data;

        // ... access other properties as needed

        // Perform further processing with the deserialized data

        // Return the response
        var response = new
        {
            status = "ok"
        };

        return Ok(response);

    }
}
