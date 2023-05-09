using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;

public class GetMyMeasurementsQuery : IRequest<MeasurementsList>
{
    public string UserId { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; } = null!;
    public GetMyMeasurementsQuery(IOptions<AppConfig> options) =>
        Options = options;
}
