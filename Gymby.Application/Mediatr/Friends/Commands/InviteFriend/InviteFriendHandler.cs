using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

public class InviteFriendHandler
    : IRequestHandler<InviteFriendCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public InviteFriendHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<string> Handle(InviteFriendCommand command,
        CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .Where(p => p.Username == command.Username)
            .FirstOrDefaultAsync(cancellationToken);

        if (profile == null)
        {
            throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Profile));
        }

        var friendship = await _dbContext.Friends
            .Where(f => ((f.ReceiverId == profile.UserId && f.SenderId == command.UserId) || (f.ReceiverId == command.UserId && f.SenderId == profile.UserId)))
            .FirstOrDefaultAsync(cancellationToken);

        if(friendship != null)
        {
            throw new InviteFriendException();
        }

        var friend = new Friend()
        {
            Id = Guid.NewGuid().ToString(),
            SenderId = command.UserId,
            ReceiverId = profile.UserId,
            Status = Status.Pending
        };

        await _dbContext.Friends.AddAsync(friend, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return friend.Id;
    }
}
