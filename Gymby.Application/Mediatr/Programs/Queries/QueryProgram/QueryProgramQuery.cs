using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.QueryProgram;

public class QueryProgramQuery : IRequest<List<ProgramVm>>
{
    public string? Level { get; set; }
    public string? Type { get; set; }
    public string? Query { get; set; }
    public string UserId { get; set; } = null!;
}
