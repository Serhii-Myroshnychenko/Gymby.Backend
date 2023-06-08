using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.UpdateDiaryExercise;

public class UpdateDiaryExerciseHandler 
    : IRequestHandler<UpdateDiaryExerciseCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateDiaryExerciseHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ExerciseVm> Handle(UpdateDiaryExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var exercisePrototype = await _dbContext.ExercisePrototypes
            .FirstOrDefaultAsync(e => e.Id == request.ExercisePrototypeId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExercisePrototypeId, nameof(ExercisePrototype));

        exercise.Name = request.Name;
        exercise.ExercisePrototypeId = request.ExercisePrototypeId;
        exercise.ExercisePrototype = exercisePrototype;
        exercise.Approaches = await _dbContext.Approaches
            .Where(a => a.ExerciseId == exercise.Id)
            .OrderBy(a => a.CreationDate)
            .ToListAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(exercise);

        if (result.Approaches == null)
        {
            result.Approaches = new List<ApproachVm>();
        }

        return result;
    }
}
