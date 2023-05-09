using Gymby.Domain.Enums;
using MediatR;

namespace Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;

public class AddMeasurementCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }
}
