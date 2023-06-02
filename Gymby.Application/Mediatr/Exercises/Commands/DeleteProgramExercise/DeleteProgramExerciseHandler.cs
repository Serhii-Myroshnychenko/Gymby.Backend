using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.DeleteProgramExercise;

public class DeleteProgramExerciseHandler 
    : IRequestHandler<DeleteProgramExerciseCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public DeleteProgramExerciseHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<Unit> Handle(DeleteProgramExerciseCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to delete an exercise");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to delete an exercise in this program");

        var programExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        if (programExercise.Approaches != null && programExercise.Approaches.Count > 0)
        {
            foreach (var approach in  programExercise.Approaches)
            {
                _dbContext.Approaches.Remove(approach);
            }
        }

        _dbContext.Exercises.Remove(programExercise);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
