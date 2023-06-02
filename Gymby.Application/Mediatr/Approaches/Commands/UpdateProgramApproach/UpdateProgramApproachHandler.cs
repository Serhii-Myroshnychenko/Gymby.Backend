using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.UpdateProgramApproach;

public class UpdateProgramApproachHandler
    : IRequestHandler<UpdateProgramApproachCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateProgramApproachHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(UpdateProgramApproachCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to update an approach");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to update an approach in this program");

        var programExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var approach = await _dbContext.Approaches
            .FirstOrDefaultAsync(a => a.Id ==  request.ApproachId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ApproachId, nameof(Approach));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        approach.Repeats = request.Repeats;
        approach.Weight = request.Weight;
        approach.IsDone = request.IsDone;
        approach.ExerciseId = request.ExerciseId;
        approach.Exercise = programExercise;

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(programExercise);

        result.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
            .Where(a => a.ExerciseId == request.ExerciseId).ToListAsync(cancellationToken));

        return result;
    }
}
