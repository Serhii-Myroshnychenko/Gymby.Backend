using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Commands.UpdateProgram;

public class UpdateProgramCommand : IRequest<ProgramVm>
{
    public string UserId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Description { get;set; } = null!;
    public string Level { get; set; } = null!;
    public string Type { get; set; } = null!;
}
