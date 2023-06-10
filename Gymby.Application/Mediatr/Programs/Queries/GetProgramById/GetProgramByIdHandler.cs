using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Queries.GetProgramById;

public class GetProgramByIdHandler 
    : IRequestHandler<GetProgramByIdQuery, ProgramVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProgramByIdHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ProgramVm> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
    {
        var program = await _dbContext.Programs
            .Include(c => c.ProgramDays)
                .ThenInclude(c => c.Exercises)!
                    .ThenInclude(c => c.Approaches)
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        if (program.IsPublic ==  false)
        {
            var accessProgram = await _dbContext.ProgramAccesses
                .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId, cancellationToken)
                ?? throw new InsufficientRightsException("You do not have permissions to view this program");
        }

        program.ProgramDays = program.ProgramDays.OrderBy(p => p.Name).ToList();

        if (program.ProgramDays != null && program.ProgramDays.Count > 0)
        {
            foreach (var programDay in program.ProgramDays)
            {
                if (programDay.Exercises != null && programDay.Exercises.Count > 0)
                {
                    programDay.Exercises = programDay.Exercises.OrderBy(e => e.Date).ToList();

                    foreach (var exercise in programDay.Exercises)
                    {
                        if (exercise.Approaches != null && exercise.Approaches.Count > 0)
                        {
                            exercise.Approaches = exercise.Approaches.OrderBy(e => e.CreationDate).ToList();
                        }
                    }
                }
            }
        }

        return _mapper.Map<ProgramVm>(program);
    }
}
