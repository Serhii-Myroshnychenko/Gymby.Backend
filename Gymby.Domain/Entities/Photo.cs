namespace Gymby.Domain.Entities;
public class Photo
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string PhotoPath { get; set; } = null!;
    public bool IsMeasurement { get; set; }
    public DateTime? MeasurementDate { get; set; }
    public DateTime CreationDate { get; set; }
}
