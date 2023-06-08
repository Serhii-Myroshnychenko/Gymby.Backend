using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.DeleteDiaryApproach;

public class DeleteDiaryApproachHandler 
    : IRequestHandler<DeleteDiaryApproachCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public DeleteDiaryApproachHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(DeleteDiaryApproachCommand request, CancellationToken cancellationToken)
    {
        var approach = await _dbContext.Approaches
            .FirstOrDefaultAsync(c => c.Id == request.ApproachId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ApproachId, nameof(Approach));

        var exercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(e => e.Id == approach.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ApproachId, nameof(Approach));

        _dbContext.Approaches.Remove(approach);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(exercise);

        result.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
            .Where(ex => ex.ExerciseId == exercise.Id)
            .OrderBy(a => a.CreationDate)
            .ToListAsync(cancellationToken));

        return result;
    }
}
