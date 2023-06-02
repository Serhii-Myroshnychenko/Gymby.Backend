using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Queries.GetProgramsFromCoach;

public class GetProgramsFromCoachHandler 
    : IRequestHandler<GetProgramsFromCoachQuery, List<ProgramFromCoachVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProgramsFromCoachHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProgramFromCoachVm>> Handle(GetProgramsFromCoachQuery request, CancellationToken cancellationToken)
    {
        var usersProgramsIds = await _dbContext.ProgramAccesses
            .Where(p => p.UserId == request.UserId && p.Type == AccessType.Shared)
            .Select(c => c.ProgramId)
            .ToListAsync(cancellationToken);

        var programOwners = await _dbContext.ProgramAccesses
            .Where(p => usersProgramsIds.Contains(p.ProgramId) && p.Type == AccessType.Owner)
            .GroupBy(p => p.ProgramId)
            .ToDictionaryAsync(g => g.Key, g => g.First().UserId, cancellationToken);

        var programs = await _dbContext.Programs
            .Where(p => usersProgramsIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<ProgramFromCoachVm>>(programs);

        foreach (var programFromCoach in result)
        {
            if (programOwners.TryGetValue(programFromCoach.Id, out var coachId))
            {
                programFromCoach.CoachUsername = await _dbContext.Profiles
                    .Where(p => p.UserId == coachId)
                    .Select(c => c.Username)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? throw new NotFoundEntityException(coachId, nameof(Domain.Entities.Profile));
            }
        }

        return result;
    }
}
