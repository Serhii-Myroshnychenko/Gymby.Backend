using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Commands.DeleteProgram;

public class DeleteProgramHandler 
    : IRequestHandler<DeleteProgramCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProgramHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _dbContext.Programs
            .Include(p => p.ProgramDays)
                .ThenInclude(p => p.Exercises)!
                    .ThenInclude(p => p.Approaches)
                        .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
                            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program)); 

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken);

        if(program.ProgramDays != null && program.ProgramDays.Count > 0)
        {
            foreach (var day in program.ProgramDays)
            {
                if(day.Exercises != null && day.Exercises.Count > 0)
                {
                    foreach (var exercise in day.Exercises)
                    {
                        if(exercise.Approaches != null && exercise.Approaches.Count > 0)
                        {
                            foreach (var approach in exercise.Approaches)
                            {
                                _dbContext.Approaches.Remove(approach);
                            }
                        }
                        _dbContext.Exercises.Remove(exercise);
                    }
                }
                _dbContext.ProgramDays.Remove(day);
            }
        }

        var programAcceses = await _dbContext.ProgramAccesses
            .Where(p => p.ProgramId == request.ProgramId)
                .ToListAsync(cancellationToken);

        _dbContext.ProgramAccesses.RemoveRange(programAcceses);
        _dbContext.Programs.Remove(program);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
