using Gymby.Application.CommandModels;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Gymby.Application.Mediatr.Payments.Commands.HandleSubscription;

public class HandleSubscriptionHandler 
    : IRequestHandler<HandleSubscriptionCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public HandleSubscriptionHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(HandleSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken)
                ?? throw new NotFoundEntityException(request.Username,nameof(Profile));

        byte[] dataBytes = Convert.FromBase64String(request.Data);

        string decodedData = Encoding.UTF8.GetString(dataBytes);

        PaymentData paymentData = JsonConvert.DeserializeObject<PaymentData>(decodedData, PaymentData.GetJsonSerializerSettings())!;

        if(paymentData != null && !string.IsNullOrEmpty(paymentData.Status))
        {
            switch (paymentData.Status)
            {
                case "subscribed":
                    profile.IsCoach = true;
                    break;
                case "unsubscribed":
                    profile.IsCoach = false;
                    break;
                default:
                    break;
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
