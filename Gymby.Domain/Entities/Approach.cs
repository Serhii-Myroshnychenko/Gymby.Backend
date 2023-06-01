namespace Gymby.Domain.Entities;

public class Approach
{
    public string Id { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual Exercise Exercise { get; set; } = null!;
}
