﻿using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.Friends.Commands.InviteFriend;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Gymby.Application.Common.Exceptions;
using Gymby.Domain.Enums;

namespace Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

public class AcceptFriendshipHandler
    : IRequestHandler<AcceptFriendshipCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public AcceptFriendshipHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<string> Handle(AcceptFriendshipCommand command, 
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

        if(friendship == null)
        {
            throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Friend));
        }

        friendship.Status = Status.Confirmed;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return friendship.Id;
    }
}
