using Gymby.Application.CommandModels;
using Gymby.Domain.Enums;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Commands.CreateProgram;

public class CreateProgramCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Level Level { get; set; }
    public ProgramType Type { get; set; }
    public List<ProgramDayCM>? ProgramDays { get; set; }
}
