using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;

public class AccessProgramToUserByUsernameHandler 
    : IRequestHandler<AccessProgramToUserByUsernameQuery, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public AccessProgramToUserByUsernameHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(AccessProgramToUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.IsCoach == true, cancellationToken)
            ?? throw new NotFoundEntityException("There is no such a user or he is not a coach", nameof(Profile));

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException($"The user with id {request.UserId} does not have such a program or can not manage it");

        var profileWhichGetAccessToNewProgram = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.Username == request.Username, cancellationToken)
            ?? throw new NotFoundEntityException("There is no such a user", nameof(Profile));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(c => c.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException("There is no such a program", nameof(Profile));

        var access = new ProgramAccess()
        {
            Id = Guid.NewGuid().ToString(),
            Type = AccessType.Shared,
            UserId = profileWhichGetAccessToNewProgram.UserId,
            IsFavorite = false,
            Program = program,
            ProgramId = program.Id
        };

        await _dbContext.ProgramAccesses.AddAsync(access, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
