using MediatR;

namespace Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;

public class DeleteMeasurementCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public string Id { get; set; } = null!;
}