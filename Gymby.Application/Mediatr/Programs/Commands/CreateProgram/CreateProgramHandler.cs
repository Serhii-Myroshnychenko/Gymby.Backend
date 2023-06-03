﻿using AutoMapper;
using Azure.Core;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Commands.CreateProgram;

public class CreateProgramHandler 
    : IRequestHandler<CreateProgramCommand, ProgramVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateProgramHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ProgramVm> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var trainerProfile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.IsCoach == true, cancellationToken)
            ?? throw new InsufficientRightsException("You do not have permissions to create a program");

        var programId = Guid.NewGuid().ToString();
        var program = new Program
        {
            Id = programId,
            Name = request.Name,
            IsPublic = false,
            Description = request.Description!,
            Level = request.Level,
            Type = request.Type
        };

        var programAccess = new ProgramAccess
        {
            Id = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            ProgramId = programId,
            Type = AccessType.Owner,
            IsFavorite = false,
            Program = program
        };

        var programDays = new List<ProgramDay>();
        var exercises = new List<Exercise>();
        var approaches = new List<Approach>();

        if (request.ProgramDays != null)
        {
            foreach (var programDayRequest in request.ProgramDays)
            {
                var programDayId = Guid.NewGuid().ToString();
                var programDay = new ProgramDay
                {
                    Id = programDayId,
                    ProgramId = programId,
                    Name = programDayRequest.Name,
                    Program = program,
                };
                programDays.Add(programDay);

                if (programDayRequest.Exercises != null)
                {
                    foreach (var exerciseRequest in programDayRequest.Exercises)
                    {
                        var exerciseId = Guid.NewGuid().ToString();
                        var exercise = new Exercise
                        {
                            Id = exerciseId,
                            ExercisePrototypeId = exerciseRequest.ExercisePrototypeId,
                            ProgramDayId = programDayId,
                            Name = exerciseRequest.Name,
                            ProgramDay = programDay
                        };
                        exercises.Add(exercise);

                        if (exerciseRequest.Approaches != null)
                        {
                            foreach (var approachRequest in exerciseRequest.Approaches)
                            {
                                var approach = new Approach
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    ExerciseId = exerciseId,
                                    Repeats = approachRequest.Repeats,
                                    Weight = approachRequest.Weight,
                                    IsDone = false,
                                    CreationDate = DateTime.Now,
                                    Exercise = exercise
                                };
                                approaches.Add(approach);
                            }
                        }
                    }
                }
            }
        }


        await _dbContext.Programs.AddAsync(program, cancellationToken);
        await _dbContext.ProgramAccesses.AddAsync(programAccess, cancellationToken);
        await _dbContext.ProgramDays.AddRangeAsync(programDays, cancellationToken);
        await _dbContext.Exercises.AddRangeAsync(exercises, cancellationToken);
        await _dbContext.Approaches.AddRangeAsync(approaches, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProgramVm>(program);
    }
}