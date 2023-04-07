using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;

public class Measurement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }
}
