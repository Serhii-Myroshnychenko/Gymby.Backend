using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;

public class GetAllExercisePrototypesQuery : IRequest<List<ExercisePrototypeVm>>
{
    public string UserId { get; set; } = null!;
}
