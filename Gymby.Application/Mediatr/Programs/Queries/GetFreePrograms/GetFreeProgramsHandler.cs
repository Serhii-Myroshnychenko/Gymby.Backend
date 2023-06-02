using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Queries.GetFreePrograms;

public class GetFreeProgramsHandler 
    : IRequestHandler<GetFreeProgramsQuery, List<ProgramVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetFreeProgramsHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProgramVm>> Handle(GetFreeProgramsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<ProgramVm>>(await _dbContext.Programs
            .Where(p => p.IsPublic == true)
            .ToListAsync(cancellationToken));
    }
}
