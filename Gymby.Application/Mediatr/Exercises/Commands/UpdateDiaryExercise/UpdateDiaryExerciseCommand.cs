using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Exercises.Commands.UpdateDiaryExercise;

public class UpdateDiaryExerciseCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
