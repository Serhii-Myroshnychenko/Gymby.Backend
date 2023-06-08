using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;

public class UpdateProgramExerciseHandler 
    : IRequestHandler<UpdateProgramExerciseCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateProgramExerciseHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(UpdateProgramExerciseCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to update an exercise");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to update an exercise in this program");

        var exercisePrototype = await _dbContext.ExercisePrototypes
            .FirstOrDefaultAsync(e => e.Id == request.ExercisePrototypeId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExercisePrototypeId, nameof(ExercisePrototype));

        var programExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        programExercise.Name = request.Name;
        programExercise.ExercisePrototypeId = request.ExercisePrototypeId;
        programExercise.ExercisePrototype = exercisePrototype;
        programExercise.Approaches = await _dbContext.Approaches
            .Where(a => a.ExerciseId == programExercise.Id)
            .OrderBy(a => a.CreationDate)
            .ToListAsync(cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(programExercise);
        
        if(result.Approaches == null)
        {
            result.Approaches = new List<ApproachVm>();
        }

        return result;
    }
}
