namespace Gymby.WebApi.Models.DiaryApproachDtos;

public class CreateDiaryApproachDto
{
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public int Interval { get; set; }
}
