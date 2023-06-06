using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;

public class GetExercisesDoneCountByDateQuery 
    : IRequest<List<ExercisesDoneCountVm>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UserId { get; set; } = null!;
}
