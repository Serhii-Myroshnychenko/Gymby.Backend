using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.GetProgramById;

public class GetProgramByIdQuery : IRequest<ProgramVm>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
}
