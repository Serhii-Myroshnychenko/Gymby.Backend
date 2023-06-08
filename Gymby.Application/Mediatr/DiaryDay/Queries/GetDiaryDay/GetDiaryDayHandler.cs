using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.DiaryDay.Queries.GetDiaryDay;

public class GetDiaryDayHandler
    : IRequestHandler<GetDiaryDayCommand, DiaryDayVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDiaryDayHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<DiaryDayVm> Handle(GetDiaryDayCommand request, CancellationToken cancellationToken)
    {
        if(request.DiaryId == null)
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(a => a.UserId == request.UserId && a.Type == AccessType.Owner, cancellationToken);

            var diaryDay = await _dbContext.DiaryDays
                .Include(d => d.Exercises)!
                    .ThenInclude(e => e.Approaches)
                        .FirstOrDefaultAsync(d => d.DiaryId == diaryAccess!.DiaryId && d.Date == request.Date.Date, cancellationToken);

            if (diaryDay != null)
            {
                if (diaryDay.Exercises != null && diaryDay.Exercises.Count > 0)
                {
                    diaryDay.Exercises = diaryDay.Exercises.OrderBy(e => e.Date).ToList();

                    foreach (var exercise in diaryDay.Exercises)
                    {
                        exercise.Approaches = exercise.Approaches.OrderBy(a => a.CreationDate).ToList();
                    }
                }
            }
            return _mapper.Map<DiaryDayVm>(diaryDay);
        }

        var result = await _dbContext.DiaryDays
            .Include(d => d.Exercises)!
                .ThenInclude(e => e.Approaches)
                    .FirstOrDefaultAsync(d => d.DiaryId == request.DiaryId && d.Date == request.Date.Date, cancellationToken);

        if (result != null)
        {
            if (result.Exercises != null && result.Exercises.Count > 0)
            {
                result.Exercises = result.Exercises.OrderBy(e => e.Date).ToList();

                foreach (var exercise in result.Exercises)
                {
                    exercise.Approaches = exercise.Approaches.OrderBy(a => a.CreationDate).ToList();
                }
            }
        }
        return _mapper.Map<DiaryDayVm>(result);
    }
}
