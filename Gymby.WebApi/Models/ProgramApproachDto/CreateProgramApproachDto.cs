namespace Gymby.WebApi.Models.ProgramApproachDto;

public class CreateProgramApproachDto
{
    public string ProgramId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public int Interval { get; set; }
}
