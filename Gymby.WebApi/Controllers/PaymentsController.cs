using Gymby.Application.Mediatr.Payments.Commands.HandleSubscription;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentsController : BaseController
{
    [HttpPost("webhook")]
    public async Task<IActionResult> SubscriptionWebhook(string username, [FromForm] string data, [FromForm] string signature)
    {
        var command = new HandleSubscriptionCommand()
        {
             Data = data,
             Username = username
        };

        return Ok(await Mediator.Send(command));
    }
}
