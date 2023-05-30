using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;

public class RejectFriendshipHandler : IRequestHandler<RejectFriendshipCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public RejectFriendshipHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<string> Handle(RejectFriendshipCommand command,
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
            .Where(f => f.SenderId == profile.UserId && f.ReceiverId == command.UserId && f.Status == Status.Pending)
            .FirstOrDefaultAsync(cancellationToken);

        if (friendship == null)
        {
            throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Friend));
        }

        friendship.Status = Status.Rejected;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return friendship.Id;
    }
}
