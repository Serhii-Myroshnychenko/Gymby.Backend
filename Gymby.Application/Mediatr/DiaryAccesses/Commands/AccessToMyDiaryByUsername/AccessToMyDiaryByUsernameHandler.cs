using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;

public class AccessToMyDiaryByUsernameHandler
    : IRequestHandler<AccessToMyDiaryByUsernameCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public AccessToMyDiaryByUsernameHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(AccessToMyDiaryByUsernameCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken)
            ?? throw new NotFoundEntityException(request.Username, nameof(Domain.Entities.Profile));

        var myDiaryAccess = await _dbContext.DiaryAccess
            .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
            ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

        var diary = await _dbContext.Diaries
            .FirstOrDefaultAsync(d => d.Id == myDiaryAccess.DiaryId, cancellationToken)
            ?? throw new NotFoundEntityException(myDiaryAccess.DiaryId, nameof(Diary));

        var diaryAccess = new DiaryAccess()
        {
            Id = Guid.NewGuid().ToString(),
            Diary = diary,
            DiaryId = diary.Id,
            Type = AccessType.Shared,
            UserId = profile.UserId
        };

        await _dbContext.DiaryAccess.AddAsync(diaryAccess, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
