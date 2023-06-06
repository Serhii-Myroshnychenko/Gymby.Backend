namespace Gymby.WebApi.Models.ProgramApproachDto;

public class UpdateProgramApproachDto
{
    public string ProgramId { get; set; } = null!;
    public string ApproachId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public bool IsDone { get; set; }
    public int Interval { get; set; }
}
