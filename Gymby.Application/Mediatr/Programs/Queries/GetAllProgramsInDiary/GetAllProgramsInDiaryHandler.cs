using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Queries.GetAllProgramsInDiary;

public class GetAllProgramsInDiaryHandler 
    : IRequestHandler<GetAllProgramsInDiaryQuery, List<ProgramVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllProgramsInDiaryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ProgramVm>> Handle(GetAllProgramsInDiaryQuery request, CancellationToken cancellationToken)
    {
        var freePrograms = await _dbContext.Programs
             .Where(p => p.IsPublic == true)
                 .Include(c => c.ProgramDays)
                     .ThenInclude(c => c.Exercises)!
                         .ThenInclude(c => c.Approaches)
                             .ToListAsync(cancellationToken);

        var programsAccessesFromCoach = await _dbContext.ProgramAccesses
            .Where(p => p.UserId == request.UserId && p.Type == AccessType.Shared)
                .Select(c => c.ProgramId)
                    .ToListAsync(cancellationToken);

        var programsFromCoaches = await _dbContext.Programs
            .Include(p => p.ProgramDays)
                .ThenInclude(p => p.Exercises)!
                    .ThenInclude(p => p.Approaches)
                        .Where(p => programsAccessesFromCoach.Contains(p.Id))
                            .ToListAsync(cancellationToken);

        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.IsCoach == true, cancellationToken);

        if(profile != null)
        {
            var programsId = await _dbContext.ProgramAccesses
                .Where(p => p.UserId == request.UserId && p.Type == AccessType.Owner)
                    .Select(c => c.ProgramId)
                        .ToListAsync(cancellationToken);

            var personalPrograms = await _dbContext.Programs
                .Include(p => p.ProgramDays)
                    .ThenInclude(p => p.Exercises)!
                        .ThenInclude (p => p.Approaches)
                            .Where(p => programsId.Contains(p.Id))
                                .ToListAsync(cancellationToken);

            programsFromCoaches.AddRange(personalPrograms);
        }

        programsFromCoaches.AddRange(freePrograms);

        return _mapper.Map<List<ProgramVm>>(programsFromCoaches);
    }
}
