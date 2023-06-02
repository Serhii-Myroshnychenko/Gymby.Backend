using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;

public class UpdateProgramExerciseCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string ExerciseId { get; set;} = null!;
    public string ExercisePrototypeId { get; set;} = null!;
    public string Name { get; set; } = null!;
}
