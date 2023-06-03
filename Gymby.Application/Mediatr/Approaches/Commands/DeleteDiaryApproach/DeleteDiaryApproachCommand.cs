using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Approaches.Commands.DeleteDiaryApproach;

public class DeleteDiaryApproachCommand : IRequest<ExerciseVm>
{
    public string UserId { get; set; } = null!;
    public string ApproachId { get; set; } = null!;
}
