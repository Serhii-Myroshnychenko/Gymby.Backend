using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;

public class DeleteProgramApproachHandler 
    : IRequestHandler<DeleteProgramApproachCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteProgramApproachHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<Unit> Handle(DeleteProgramApproachCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to delete an approach");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to delete an approach in this program");

        var programExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var approach = await _dbContext.Approaches
            .FirstOrDefaultAsync(a => a.Id == request.ApproachId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ApproachId, nameof(Approach));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        _dbContext.Approaches.Remove(approach);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
