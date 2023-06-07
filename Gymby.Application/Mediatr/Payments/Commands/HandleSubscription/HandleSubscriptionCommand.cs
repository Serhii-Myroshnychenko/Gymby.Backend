using MediatR;

namespace Gymby.Application.Mediatr.Payments.Commands.HandleSubscription;

public class HandleSubscriptionCommand : IRequest<Unit>
{
    public string Username { get; set; } = null!;
    public string Data { get; set;} = null!;
}
