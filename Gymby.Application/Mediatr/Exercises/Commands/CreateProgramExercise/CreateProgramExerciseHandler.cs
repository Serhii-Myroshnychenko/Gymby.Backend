using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;

public class CreateProgramExerciseHandler
    : IRequestHandler<CreateProgramExerciseCommand, ExerciseVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateProgramExerciseHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ExerciseVm> Handle(CreateProgramExerciseCommand request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId && p.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create an exercise");

        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create an exercise in this program");

        var programDay = await _dbContext.ProgramDays
            .FirstOrDefaultAsync(p => p.Id == request.ProgramDayId, cancellationToken) 
            ?? throw new NotFoundEntityException(request.ProgramDayId, nameof(ProgramDay));

        var exercisePrototype = await _dbContext.ExercisePrototypes
            .FirstOrDefaultAsync(e => e.Id == request.ExercisePrototypeId, cancellationToken)
            ?? throw new NotFoundEntityException(request.ExercisePrototypeId, nameof(ExercisePrototype));

        var exercise = new Exercise()
        {
            Id = Guid.NewGuid().ToString(),
            ProgramDayId = programDay.Id,
            Approaches = new List<Approach>(),
            ProgramDay = programDay,
            ExercisePrototypeId = request.ExercisePrototypeId,
            ExercisePrototype = exercisePrototype,
            Name = request.Name,
            Date = DateTime.Now,
        };

        if(programDay.Exercises == null)
        {
            programDay.Exercises = new List<Exercise>
            {
                exercise
            };
        }
        else
        {
            programDay.Exercises.Add(exercise);
        }
        
        await _dbContext.Exercises.AddAsync(exercise, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ExerciseVm>(exercise);

        result.Approaches = new List<ApproachVm>();

        return result;
    }
}
