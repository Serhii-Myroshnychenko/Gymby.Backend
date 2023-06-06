using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Approaches.Commands.CreateDiaryApproach;

public class CreateDiaryApproachCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public int Interval { get; set; }
}
