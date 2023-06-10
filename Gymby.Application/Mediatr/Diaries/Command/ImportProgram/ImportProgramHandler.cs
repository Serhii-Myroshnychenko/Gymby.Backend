using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;
using Gymby.Application.Utils;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Diaries.Command.ImportProgram;

public class ImportProgramHandler 
    : IRequestHandler<ImportProgramCommand, Unit>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMediator _mediator;

    public ImportProgramHandler(IApplicationDbContext dbContext, IMediator mediator) =>
        (_dbContext, _mediator) = (dbContext, mediator);

    public async Task<Unit> Handle(ImportProgramCommand request, CancellationToken cancellationToken)
    {
        if (request.DiaryId == null)
        {
            var diaryAccess = await _dbContext.DiaryAccess
                .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Type == AccessType.Owner, cancellationToken)
                ?? throw new NotFoundEntityException(request.UserId, nameof(DiaryAccess));

            var program = await _dbContext.Programs
                .Include(p => p.ProgramDays)
                    .ThenInclude(p => p.Exercises)!
                        .ThenInclude(p => p.Approaches)
                            .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
                                ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

            if (program.ProgramDays != null && program.ProgramDays.Count > 0)
            {
                var date = request.StartDate;
                foreach (var programDay in program.ProgramDays)
                {
                    var currentDay = DateHandler.GetNameOfDay(date);
                    while (!request.DaysOfWeek.Contains(currentDay))
                    {
                        date = date.AddDays(1);
                        currentDay = DateHandler.GetNameOfDay(date);
                    }
                    await _mediator.Send(new ImportProgramDayCommand()
                    {
                        Date = date,
                        DiaryId = null,
                        ProgramDayId = programDay.Id,
                        ProgramId = programDay.ProgramId,
                        UserId = request.UserId,
                    }, cancellationToken);
                    date = date.AddDays(1);
                }
            }

        }
        else
        {
            var diary = await _dbContext.Diaries
                .FirstOrDefaultAsync(d => d.Id == request.DiaryId, cancellationToken)
                    ?? throw new NotFoundEntityException(request.DiaryId, nameof(Diary));

            var program = await _dbContext.Programs
               .Include(p => p.ProgramDays)
                   .ThenInclude(p => p.Exercises)!
                       .ThenInclude(p => p.Approaches)
                           .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken)
                               ?? throw new NotFoundEntityException(request.ProgramId, nameof(Program));

            if (program.ProgramDays != null && program.ProgramDays.Count > 0)
            {
                var date = request.StartDate;
                foreach (var programDay in program.ProgramDays)
                {
                    var currentDay = DateHandler.GetNameOfDay(date);
                    while (!request.DaysOfWeek.Contains(currentDay))
                    {
                        date = date.AddDays(1);
                        currentDay = DateHandler.GetNameOfDay(date);
                    }
                    await _mediator.Send(new ImportProgramDayCommand()
                    {
                        Date = date,
                        DiaryId = diary.Id,
                        ProgramDayId = programDay.Id,
                        ProgramId = programDay.ProgramId,
                        UserId = request.UserId,
                    }, cancellationToken);
                    date = date.AddDays(1);
                }
            }


        }
        return Unit.Value;
    }
}