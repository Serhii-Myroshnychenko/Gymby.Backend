using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;

public class GetAllExercisePrototypesHandler
: IRequestHandler<GetAllExercisePrototypesQuery, List<ExercisePrototypeVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public GetAllExercisePrototypesHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<ExercisePrototypeVm>> Handle(GetAllExercisePrototypesQuery request, CancellationToken cancellationToken)
    {
         return _mapper.Map<List<ExercisePrototypeVm>>(await _dbContext.ExercisePrototypes.ToListAsync(cancellationToken));
    }
}
