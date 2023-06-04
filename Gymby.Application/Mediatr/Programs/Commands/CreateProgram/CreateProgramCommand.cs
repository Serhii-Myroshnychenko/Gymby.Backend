using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Commands.CreateProgram;

public class CreateProgramCommand : IRequest<ProgramVm>
{
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Level { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<ProgramDayCM>? ProgramDays { get; set; }
}
