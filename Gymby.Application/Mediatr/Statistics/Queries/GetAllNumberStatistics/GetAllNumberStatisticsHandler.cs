using AutoMapper;
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
    private readonly IMapper _mapper;

    public GetAllNumberStatisticsHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<NumericStatisticsVm> Handle(GetAllNumberStatisticsQuery request, CancellationToken cancellationToken)
    {
        var diaryAccess = await _dbContext.DiaryAccess
        .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
        ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

        var statistics = await _dbContext.DiaryDays
            .Include(d => d.Exercises)!
                .ThenInclude(e => e.Approaches)
            .Where(d => d.DiaryId == diaryAccess.DiaryId)
            .SelectMany(d => d.Exercises!)
            .ToListAsync(cancellationToken);

        int countOfExecutedExercises = statistics
            .Count(exercise => exercise.Approaches.All(approach => approach.IsDone));

        int countOfTrainings = statistics.Count;

        int countOfExecutedApproaches = statistics
            .SelectMany(exercise => exercise.Approaches)
            .Count(approach => approach.IsDone);

        int maxApproachesCountPerTraining = statistics
            .Max(exercise => exercise.Approaches.Sum(approach => approach.Repeats));

        double maxTonnagePerTraining = statistics
            .SelectMany(exercise => exercise.Approaches)
            .Where(approach => approach.IsDone)
            .Sum(approach => approach.Weight);

        int maxExercisesCountPerTraining = statistics
            .GroupBy(exercise => exercise.DiaryDayId)
            .Max(group => group.Count());

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
