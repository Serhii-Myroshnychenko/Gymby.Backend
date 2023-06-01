using MediatR;

namespace Gymby.Application.Mediatr.Exercises.Commands.DeleteProgramExercise;

public class DeleteProgramExerciseCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
}
