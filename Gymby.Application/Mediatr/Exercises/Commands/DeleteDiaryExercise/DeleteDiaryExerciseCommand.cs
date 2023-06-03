using MediatR;

namespace Gymby.Application.Mediatr.Exercises.Commands.DeleteDiaryExercise;

public class DeleteDiaryExerciseCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
}
