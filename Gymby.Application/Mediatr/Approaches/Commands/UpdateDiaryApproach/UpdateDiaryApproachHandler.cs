using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.UpdateDiaryApproach;

public class UpdateDiaryApproachHandler 
    : IRequestHandler<UpdateDiaryApproachCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateDiaryApproachHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(UpdateDiaryApproachCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(c => c.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var approach = await _dbContext.Approaches
            .FirstOrDefaultAsync(c => c.Id == request.ApproachId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ApproachId, nameof(Approach));

        approach.Weight = request.Weight;
        approach.Repeats = request.Repeats;
        approach.IsDone = request.IsDone;
        approach.Interval = request.Interval;
        approach.Exercise = exercise;
        approach.ExerciseId = exercise.Id;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(exercise);

        result.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
            .Where(a => a.ExerciseId == request.ExerciseId)
            .OrderBy(a => a.CreationDate)
            .ToListAsync(cancellationToken));

        return result;
    }
}
