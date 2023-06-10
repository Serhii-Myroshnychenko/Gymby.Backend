using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Statistics.Queries.GetAllNumberStatistics;

public class GetAllNumberStatisticsHandler 
    : IRequestHandler<GetAllNumberStatisticsQuery, NumericStatisticsVm>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllNumberStatisticsHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<NumericStatisticsVm> Handle(GetAllNumberStatisticsQuery request, CancellationToken cancellationToken)
    {
        var diaryAccess = await _dbContext.DiaryAccess
            .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
                ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

        var diaryDays = await _dbContext.DiaryDays
            .Include(d => d.Exercises)!
                .ThenInclude(d => d.Approaches)
                    .Where(d => d.DiaryId == diaryAccess.DiaryId)
                         .ToListAsync(cancellationToken);

        int countOfExecutedExercises = 0;
        int countOfTrainings = 0;
        int countOfExecutedApproaches = 0;
        int maxApproachesCountPerTraining = 0;
        double maxTonnagePerTraining = 0;
        int maxExercisesCountPerTraining = 0;

        foreach (var day in diaryDays)
        {
            countOfTrainings++;
            int currentCountApproachesPerDay = 0;
            double currentTonnagePerDay = 0;
            int currentExercisesCountPerDay = 0;

            if (day.Exercises != null && day.Exercises.Count > 0)
            {
                foreach (var exercise in day.Exercises)
                {
                    if (exercise.Approaches != null && exercise.Approaches.Count > 0)
                    {
                        if (exercise.Approaches.All(a => a.IsDone == true))
                        {
                            countOfExecutedExercises++;
                            currentExercisesCountPerDay++;
                        }

                        foreach (var approach in exercise.Approaches)
                        {
                            if (approach.IsDone == true)
                            {
                                countOfExecutedApproaches++;
                                currentCountApproachesPerDay += approach.Repeats;
                                currentTonnagePerDay += (approach.Weight * approach.Repeats);
                            }
                        }
                    }
                }
            }
            maxApproachesCountPerTraining = Math.Max(maxApproachesCountPerTraining, currentCountApproachesPerDay);
            maxTonnagePerTraining = Math.Max(maxTonnagePerTraining, currentTonnagePerDay);
            maxExercisesCountPerTraining = Math.Max(maxExercisesCountPerTraining, currentExercisesCountPerDay);
        }
        return new NumericStatisticsVm()
        {
            CountOfExecutedApproaches = countOfExecutedApproaches,
            MaxApproachesCountPerTraining = maxApproachesCountPerTraining,
            MaxExercisesCountPerTraining = maxExercisesCountPerTraining,
            MaxTonnagePerTraining = maxTonnagePerTraining,
            СountOfTrainings = countOfTrainings,
            СountOfExecutedExercises = countOfExecutedExercises
        };
    }
}
