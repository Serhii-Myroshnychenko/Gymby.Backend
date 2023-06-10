using AutoMapper;
using Gymby.Application.Mediatr.Programs.Queries.GetAllProgramsInDiary;
using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.QueryProgram;

public class QueryProgramHandler 
    : IRequestHandler<QueryProgramQuery, List<ProgramVm>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public QueryProgramHandler(IMapper mapper, IMediator mediator) =>
        (_mapper, _mediator) = (mapper, mediator);

    public async Task<List<ProgramVm>> Handle(QueryProgramQuery request, CancellationToken cancellationToken)
    {
        var programs = await _mediator.Send(new GetAllProgramsInDiaryQuery() { UserId = request.UserId }, cancellationToken);

        var filteredPrograms = programs.Where(p =>
            (request.Query == null || p.Name.Contains(request.Query)) &&
            (request.Level == null || p.Level == request.Level) &&
            (request.Type == null || p.Type == request.Type)
        );

        return _mapper.Map<List<ProgramVm>>(filteredPrograms);
    }
}
