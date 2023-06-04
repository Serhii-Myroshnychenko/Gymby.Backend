using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Programs.Commands.UpdateProgram;

public class UpdateProgramHandler 
    : IRequestHandler<UpdateProgramCommand, ProgramVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateProgramHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProgramVm> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var programAccess = await _dbContext.ProgramAccesses
            .FirstOrDefaultAsync(p => p.ProgramId == request.ProgramId && p.UserId == request.UserId && p.Type == AccessType.Owner, cancellationToken)
            ?? throw new InsufficientRightsException("You can not modify this program");

        var program = await _dbContext.Programs
            .Include(p => p.ProgramDays)
                .ThenInclude(p => p.Exercises)!
                    .ThenInclude(p => p.Approaches)
                        .FirstOrDefaultAsync(p => p.Id == request.ProgramId, cancellationToken);

        program!.Description = request.Description;
        program!.Name = request.Name;
        program.Type = (ProgramType)Enum.Parse( typeof(ProgramType),request.Type);
        program.Level = (Level)Enum.Parse(typeof(Level), request.Level);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProgramVm>(program);
    }
}
