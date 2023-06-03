using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;

public class ImportProgramDayHandler
    : IRequestHandler<ImportProgramDayCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ImportProgramDayHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<Unit> Handle(ImportProgramDayCommand request, CancellationToken cancellationToken)
    {
        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        var programDay = await _dbContext.ProgramDays
                .Include(p => p.Exercises)!
                    .ThenInclude(a => a.Approaches)
            .FirstOrDefaultAsync(p => p.Id == request.ProgramDayId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramDayId, nameof(ProgramDay));

        if(request.DiaryId == null)
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken);

            var diary = await _dbContext.Diaries
                .FirstOrDefaultAsync(d => d.Id == diaryAccess!.DiaryId, cancellationToken);

            var diaryDay = await _dbContext.DiaryDays
                .FirstOrDefaultAsync(d => d.DiaryId == diaryAccess!.DiaryId && d.Date == request.Date.Date, cancellationToken);

            if(diaryDay == null)
            {
                diaryDay = new DiaryDay()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = request.Date.Date,
                    Diary = diary!,
                    DiaryId = diary!.Id,
                    Exercises = new List<Exercise>()
                };

                await _dbContext.DiaryDays.AddAsync(diaryDay, cancellationToken);
            }

            if(programDay.Exercises != null && programDay.Exercises.Count > 0)
            {
                foreach(var exercise in programDay.Exercises)
                {
                    var exer = new Exercise()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Date = request.Date,
                        Name = exercise.Name,
                        DiaryDay = diaryDay,
                        DiaryDayId = diaryDay.Id,
                        ExercisePrototype = exercise.ExercisePrototype,
                        ExercisePrototypeId = exercise.ExercisePrototypeId,
                        Approaches = new List<Approach>()
                    };

                    await _dbContext.Exercises.AddAsync(exer, cancellationToken);

                    if (exercise.Approaches != null && exercise.Approaches.Count > 0)
                    {
                        foreach (var approach in exercise.Approaches)
                        {
                            var curApproach = new Approach()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreationDate = DateTime.Now,
                                Exercise = exer,
                                ExerciseId = exer.Id,
                                IsDone = false,
                                Repeats = approach.Repeats,
                                Weight = approach.Weight,
                            };

                            await _dbContext.Approaches.AddAsync(curApproach, cancellationToken);
                        }
                    }
                }
            }

        }

        else
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(d => d.DiaryId == request.DiaryId && d.Type == AccessType.Shared, cancellationToken);

            var diary = await _dbContext.Diaries
                .FirstOrDefaultAsync(d => d.Id == diaryAccess!.DiaryId, cancellationToken);

            var diaryDay = await _dbContext.DiaryDays
                .FirstOrDefaultAsync(d => d.DiaryId == diaryAccess!.DiaryId && d.Date == request.Date.Date, cancellationToken);

            if (diaryDay == null)
            {
                diaryDay = new DiaryDay()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = request.Date.Date,
                    Diary = diary!,
                    DiaryId = diary!.Id,
                    Exercises = new List<Exercise>()
                };

                await _dbContext.DiaryDays.AddAsync(diaryDay, cancellationToken);
            }

            if (programDay.Exercises != null && programDay.Exercises.Count > 0)
            {
                foreach (var exercise in programDay.Exercises)
                {
                    var exer = new Exercise()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Date = request.Date,
                        Name = exercise.Name,
                        DiaryDay = diaryDay,
                        DiaryDayId = diaryDay.Id,
                        ExercisePrototype = exercise.ExercisePrototype,
                        ExercisePrototypeId = exercise.ExercisePrototypeId,
                        Approaches = new List<Approach>()
                    };

                    await _dbContext.Exercises.AddAsync(exer, cancellationToken);

                    if (exercise.Approaches != null && exercise.Approaches.Count > 0)
                    {
                        foreach (var approach in exercise.Approaches)
                        {
                            var curApproach = new Approach()
                            {
                                Id = Guid.NewGuid().ToString(),
                                CreationDate = DateTime.Now,
                                Exercise = exer,
                                ExerciseId = exer.Id,
                                IsDone = false,
                                Repeats = approach.Repeats,
                                Weight = approach.Weight,
                            };

                            await _dbContext.Approaches.AddAsync(curApproach, cancellationToken);
                        }
                    }
                }
            }
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}