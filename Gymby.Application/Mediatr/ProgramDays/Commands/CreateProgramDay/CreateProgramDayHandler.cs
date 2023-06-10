using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;

public class CreateProgramDayHandler 
    : IRequestHandler<CreateProgramDayCommand, ProgramDayVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateProgramDayHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ProgramDayVm> Handle(CreateProgramDayCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
             .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
             ?? throw new InsufficientRightsException("You do not have permissions to create a programDay");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create a programDay in this program");

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        var programDay = new ProgramDay()
        {
            Id = Guid.NewGuid().ToString(),
            Exercises = new List<Exercise>(),
            Program = program,
            ProgramId = request.ProgramId,
            Name = request.Name
        };

        await _dbContext.ProgramDays.AddAsync(programDay, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ProgramDayVm>(programDay);
        result.Exercises = new List<ExerciseVm>();

        return result;
    }
}
