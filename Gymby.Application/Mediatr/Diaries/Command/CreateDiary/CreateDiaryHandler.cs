using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Diaries.Command.CreateDiary;

public class CreateDiaryHandler
    : IRequestHandler<CreateDiaryCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateDiaryHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(CreateDiaryCommand request, CancellationToken cancellationToken)
    {
        var access = await _dbContext.DiaryAccess
            .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Type == AccessType.Owner, cancellationToken);

        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

        if(access == null)
        {
            var diary = new Diary()
            {
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                Name = profile!.Username + " diary"
            };

            var diaryAccess = new DiaryAccess()
            {
                Id = Guid.NewGuid().ToString(),
                Diary = diary,
                DiaryId = diary.Id,
                Type = AccessType.Owner,
                UserId = request.UserId
            };

            await _dbContext.Diaries.AddAsync(diary, cancellationToken);
            await _dbContext.DiaryAccess.AddAsync(diaryAccess, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
