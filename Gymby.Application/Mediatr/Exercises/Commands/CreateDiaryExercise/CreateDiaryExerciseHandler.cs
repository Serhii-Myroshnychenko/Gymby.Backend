using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.CreateDiaryExercise;

public class CreateDiaryExerciseHandler
    : IRequestHandler<CreateDiaryExerciseCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateDiaryExerciseHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ExerciseVm> Handle(CreateDiaryExerciseCommand request, CancellationToken cancellationToken)
    {
        var diary = await _dbContext.Diaries
            .FirstOrDefaultAsync(d => d.Id == request.DiaryId, cancellationToken)
            ?? throw new NotFoundEntityException(request.DiaryId, nameof(Diary));

        var exercisePrototype = await _dbContext.ExercisePrototypes
            .FirstOrDefaultAsync(e => e.Id == request.ExercisePrototypeId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExercisePrototypeId, nameof(ExercisePrototype));

        var diaryDay = await _dbContext.DiaryDays
                .Include(d => d.Exercises)
            .FirstOrDefaultAsync(d => d.Date == request.Date.Date && d.DiaryId == diary.Id, cancellationToken);

        if(diaryDay == null)
        {
            var newDiaryDay = new Domain.Entities.DiaryDay()
            {
                Id = Guid.NewGuid().ToString(),
                Date = request.Date.Date,
                Diary = diary,
                DiaryId = diary.Id,
                Exercises = new List<Exercise>()
            };

            var exercise = new Exercise()
            {
                Id = Guid.NewGuid().ToString(),
                Date = request.Date,
                DiaryDay = newDiaryDay,
                DiaryDayId = newDiaryDay.Id,
                ExercisePrototype = exercisePrototype,
                ExercisePrototypeId = exercisePrototype.Id,
                Name = request.Name,
                Approaches = new List<Approach>()
            };

            await _dbContext.DiaryDays.AddAsync(newDiaryDay, cancellationToken);
            await _dbContext.Exercises.AddAsync(exercise, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = _mapper.Map<ExerciseVm>(exercise);
            result.Approaches = new List<ApproachVm>();

            return result;
        }

        var newExercise = new Exercise()
        {
            Id = Guid.NewGuid().ToString(),
            Date = request.Date,
            DiaryDay = diaryDay,
            DiaryDayId = diaryDay.Id,
            ExercisePrototype = exercisePrototype,
            ExercisePrototypeId = exercisePrototype.Id,
            Name = request.Name,
            Approaches = new List<Approach>()
        };

        await _dbContext.Exercises.AddAsync(newExercise, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var res = _mapper.Map<ExerciseVm>(newExercise);
        res.Approaches = new List<ApproachVm>();

        return res;

    }
}
