using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.GetProgramsFromCoach;

public class GetProgramsFromCoachQuery : IRequest<List<ProgramFromCoachVm>>
{
    public string UserId { get; set; } = null!;
}
