using MediatR;

namespace Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;

public class DeleteProgramApproachCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string ApproachId { get; set; } = null!;
    public string ExerciseId { get; set;} = null!;
}
