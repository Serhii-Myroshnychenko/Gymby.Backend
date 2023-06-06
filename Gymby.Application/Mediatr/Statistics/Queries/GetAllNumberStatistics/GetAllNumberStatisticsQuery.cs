using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Statistics.Queries.GetAllNumberStatistics;

public class GetAllNumberStatisticsQuery : IRequest<NumericStatisticsVm>
{
    public string UserId { get; set; } = null!;
}
