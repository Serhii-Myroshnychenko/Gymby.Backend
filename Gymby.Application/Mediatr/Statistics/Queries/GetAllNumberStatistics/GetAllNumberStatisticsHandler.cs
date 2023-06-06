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
        var numericStatisticsVm = new NumericStatisticsVm();

        //var diaryAccess = await _dbContext.DiaryAccess
        //    .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
        //        ?? throw new NotFoundEntityException(request.UserId,nameof(DiaryAccess));

        //var diaryDays = await _dbContext.DiaryDays
        //    .Include(d => d.Exercises)!
        //        .ThenInclude(d => d.Approaches)
        //            .Where(d => d.DiaryId == diaryAccess.DiaryId)
        //                 .ToListAsync(cancellationToken);

        //int countOfExecutedExercises = 0;
        //int countOfTrainings = 0;
        //int countOfExecutedApproaches = 0;
        //int maxApproachesCountPerTraining = 0;
        //int maxTonnagePerTrainin = 0;
        //int maxExercisesCountPerTraining = 0;

        //foreach(var day in diaryDays)
        //{
        //    countOfTrainings++;
        //    if(day.Exercises != null && day.Exercises.Count > 0)
        //    {
        //        foreach(var exercise in day.Exercises)
        //        {
        //            exercise.
        //        }
        //    }
        //}
        return numericStatisticsVm;
    }
}
