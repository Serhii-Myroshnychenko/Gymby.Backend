
namespace Gymby.Domain.Entities;
public class Photo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsMeasurement { get; set; }
    public DateTime? MeasurementDate { get; set; }
    public DateTime CreationDate { get; set; }
}
