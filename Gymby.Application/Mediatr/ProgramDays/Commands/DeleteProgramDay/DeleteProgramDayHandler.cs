using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.ProgramDays.Commands.DeleteProgramDay;

public class DeleteProgramDayHandler 
    : IRequestHandler<DeleteProgramDayCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProgramDayHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(DeleteProgramDayCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
             .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
             ?? throw new InsufficientRightsException("You do not have permissions to delete a programDay");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to delete a programDay in this program");

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        var programDay = await _dbContext.ProgramDays
                .Include(p => p.Exercises)!
                    .ThenInclude(c => c.Approaches)
            .FirstOrDefaultAsync(p => p.Id == request.ProgramDayId && p.ProgramId == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramDayId, nameof(ProgramDay));

        var approachesToDelete = new List<string>();
        var exercisesToDelete = new List<string>();
        
        if(programDay.Exercises != null && programDay.Exercises.Count > 0)
        {
            foreach (var exercise in programDay.Exercises)
            {
                if(exercise.Approaches != null && exercise.Approaches.Count > 0)
                {
                    foreach (var approach in exercise.Approaches)
                    {
                        approachesToDelete.Add(approach.Id);
                    }
                }
                exercisesToDelete.Add(exercise.Id);
            }
        }

        var approaches = await _dbContext.Approaches
            .Where(a => approachesToDelete.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var exercises = await _dbContext.Exercises
            .Where(e => exercisesToDelete.Contains(e.Id))
            .ToListAsync(cancellationToken);

        _dbContext.Approaches.RemoveRange(approaches);
        _dbContext.Exercises.RemoveRange(exercises);
        _dbContext.ProgramDays.Remove(programDay);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
