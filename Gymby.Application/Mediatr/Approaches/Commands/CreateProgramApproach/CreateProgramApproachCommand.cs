using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;

public class CreateProgramApproachCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set;} = null!;
    public string ExerciseId { get; set;} = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
}
