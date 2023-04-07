namespace Gymby.Domain.Entities;
public class Approach
{
    public int Id { get; set; }
    public int ExerciseId { get; set; }
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreationDate { get; set; }
    public Exercise Exercise { get; set; } = null!;
}
