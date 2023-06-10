using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Queries.GetPersonalPrograms;

public class GetPersonalProgramsHandler 
    : IRequestHandler<GetPersonalProgramsQuery, List<ProgramVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPersonalProgramsHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProgramVm>> Handle(GetPersonalProgramsQuery request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You are not a trainer");

        var programsId = await _dbContext.ProgramAccesses
            .Where(p => p.UserId == request.UserId && p.Type == AccessType.Owner)
            .Select(c => c.ProgramId)
            .ToListAsync(cancellationToken);

        var programs = await _dbContext.Programs
            .Where(p => programsId.Contains(p.Id))
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ProgramVm>>(programs);
    }
}
