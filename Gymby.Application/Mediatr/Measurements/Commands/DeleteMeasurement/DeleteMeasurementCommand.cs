using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;

public class DeleteMeasurementCommand : IRequest<MeasurementsList>
{
    public string UserId { get; set; } = null!;
    public string Id { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; }

    public DeleteMeasurementCommand(IOptions<AppConfig> options)
    {
        Options = options;
    }
}