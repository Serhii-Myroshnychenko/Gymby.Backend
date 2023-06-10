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

    public GetAllExercisePrototypesHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ExercisePrototypeVm>> Handle(GetAllExercisePrototypesQuery request, CancellationToken cancellationToken)
    {
         return _mapper.Map<List<ExercisePrototypeVm>>(await _dbContext.ExercisePrototypes.ToListAsync(cancellationToken));
    }
}
