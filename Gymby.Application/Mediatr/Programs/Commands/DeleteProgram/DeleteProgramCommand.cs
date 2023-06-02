using MediatR;

namespace Gymby.Application.Mediatr.Programs.Commands.DeleteProgram;

public class DeleteProgramCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
}
