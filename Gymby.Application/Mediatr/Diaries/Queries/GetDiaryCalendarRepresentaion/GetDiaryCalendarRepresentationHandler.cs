using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Diaries.Queries.GetDiaryCalendarRepresentaion;

public class GetDiaryCalendarRepresentationHandler 
    : IRequestHandler<GetDiaryCalendarRepresentationQuery, List<DiaryCalendarRepresentationVm>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetDiaryCalendarRepresentationHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<List<DiaryCalendarRepresentationVm>> Handle(GetDiaryCalendarRepresentationQuery request, CancellationToken cancellationToken)
    {
        var result = new List<DiaryCalendarRepresentationVm>();

        var query = _dbContext.DiaryDays
            .Include(d => d.Exercises)!
                .ThenInclude(d => d.Approaches)
            .Where(d => d.Date >= request.StartDate.Date && d.Date <= request.EndDate.Date);

        if (request.DiaryId == null)
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
                ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

            query = query.Where(d => d.DiaryId == diaryAccess.DiaryId);
        }
        else
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(d => d.DiaryId == request.DiaryId && d.UserId == request.UserId && d.Type == AccessType.Shared, cancellationToken)
                ?? throw new NotFoundEntityException(request.DiaryId, nameof(DiaryAccess));

            query = query.Where(d => d.DiaryId == diaryAccess.DiaryId);
        }

        var diaryDays = await query.ToListAsync(cancellationToken);

        foreach (var day in diaryDays)
        {
            var currentCalendarDay = new DiaryCalendarRepresentationVm() { Date = day.Date, DiaryDayId = day.Id };

            if (day.Exercises != null && day.Exercises.Count > 0)
            {
                var exercisePrototypeIds = day.Exercises.Select(e => e.ExercisePrototypeId).Distinct().ToList();
                var categories = await _dbContext.ExercisePrototypes
                    .Where(e => exercisePrototypeIds.Contains(e.Id))
                    .Select(e => e.Category)
                    .ToListAsync(cancellationToken);

                currentCalendarDay.Categories.AddRange(categories.Select(c => c.ToString()));
            }

            result.Add(currentCalendarDay);
        }

        return result;
    }
}
