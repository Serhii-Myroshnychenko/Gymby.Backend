using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Approaches.Commands.CreateDiaryApproach;

public class CreateDiaryApproachHandler 
    : IRequestHandler<CreateDiaryApproachCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public CreateDiaryApproachHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ExerciseVm> Handle(CreateDiaryApproachCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _dbContext.Exercises
            .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExerciseId, nameof(Exercise));

        var approach = new Approach()
        {
            Id = Guid.NewGuid().ToString(),
            CreationDate = DateTime.Now,
            IsDone = false,
            Exercise = exercise,
            ExerciseId = request.ExerciseId,
            Repeats = request.Repeats,
            Weight = request.Weight,
            Interval = request.Interval
        };

        await _dbContext.Approaches.AddAsync(approach, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(exercise);

        result.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
            .Where(ex => ex.ExerciseId == request.ExerciseId)
            .ToListAsync(cancellationToken));

        return result;
    }
}
