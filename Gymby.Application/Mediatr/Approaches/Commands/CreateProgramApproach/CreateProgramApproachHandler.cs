using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;

public class CreateProgramApproachHandler 
    : IRequestHandler<CreateProgramApproachCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public CreateProgramApproachHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(CreateProgramApproachCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create an approach");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create an approach in this program");

        var programExercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(p => p.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        var approach = new Approach()
        {
            Id = Guid.NewGuid().ToString(),
            CreationDate = DateTime.Now,
            Exercise = programExercise,
            ExerciseId = request.ExerciseId,
            IsDone = false,
            Weight = request.Weight,
            Repeats = request.Repeats,
        };

        await _dbContext.Approaches.AddAsync(approach, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(programExercise);

        result.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
            .Where(ex => ex.ExerciseId == request.ExerciseId)
            .ToListAsync(cancellationToken));
        
        return result;
    }
}
