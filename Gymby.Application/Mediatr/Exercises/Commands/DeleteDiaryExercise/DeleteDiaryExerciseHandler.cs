using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.DeleteDiaryExercise;

public class DeleteDiaryExerciseHandler
    : IRequestHandler<DeleteDiaryExerciseCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteDiaryExerciseHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(DeleteDiaryExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _dbContext.Exercises
                .Include(e => e.Approaches)
            .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        if (exercise.Approaches != null && exercise.Approaches.Count > 0)
        {
            foreach (var approach in exercise.Approaches)
            {
                _dbContext.Approaches.Remove(approach);
            }
        }

        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    } 
}
