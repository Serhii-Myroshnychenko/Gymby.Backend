using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.ProgramDays.Commands.UpdateProgramDay;

public class UpdateProgramDayHandler 
    : IRequestHandler<UpdateProgramDayCommand, ProgramDayVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateProgramDayHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ProgramDayVm> Handle(UpdateProgramDayCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
             .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
             ?? throw new InsufficientRightsException("You do not have permissions to update a programDay");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to update a programDay in this program");

        var program = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

        var programDay = await _dbContext.ProgramDays
            .FirstOrDefaultAsync(p => p.Id == request.ProgramDayId && p.ProgramId == request.ProgramId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ProgramDayId, nameof(ProgramDay));

        programDay.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ProgramDayVm>(programDay);

        result.Exercises = _mapper.Map<List<ExerciseVm>>(await _dbContext.Exercises
            .Where(e => e.ProgramDayId == request.ProgramDayId)
            .OrderBy(e => e.Date)
            .ToListAsync(cancellationToken));

        foreach (var exercise in result.Exercises)
        {
            exercise.Approaches = _mapper.Map<List<ApproachVm>>(await _dbContext.Approaches
                .Where(a => a.ExerciseId == exercise.Id)
                .OrderBy(a => a.CreationDate)
                .ToListAsync(cancellationToken));
        }

        return result;
    }
}
