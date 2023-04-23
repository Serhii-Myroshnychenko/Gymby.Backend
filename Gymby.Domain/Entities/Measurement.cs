using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;

public class Measurement
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }
}
