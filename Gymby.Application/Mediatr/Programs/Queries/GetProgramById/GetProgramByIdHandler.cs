using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.ProgramDays.Commands.UpdateProgramDay;
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
    private readonly IFileService _fileService;

    public GetProgramByIdHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

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

        return _mapper.Map<ProgramVm>(program);
    }
}
