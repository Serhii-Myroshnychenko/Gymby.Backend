using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;

public class GetExercisesDoneCountByDateHandler 
    : IRequestHandler<GetExercisesDoneCountByDateQuery, List<ExercisesDoneCountVm>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetExercisesDoneCountByDateHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<List<ExercisesDoneCountVm>> Handle(GetExercisesDoneCountByDateQuery request, CancellationToken cancellationToken)
    {
        var diaryAccess = await _dbContext.DiaryAccess
            .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
                ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

        var result = await _dbContext.DiaryDays
            .Include(d => d.Exercises)!
                .ThenInclude(e => e.Approaches)
            .Where(d => d.DiaryId == diaryAccess.DiaryId && d.Date >= request.StartDate.Date && d.Date <= request.EndDate.Date)
            .Select(day => new ExercisesDoneCountVm
            {
                Date = day.Date,
                Value = day.Exercises!.Count(exercise => exercise.Approaches.All(approach => approach.IsDone))
            })
            .ToListAsync(cancellationToken);

        return result;
    }
}
