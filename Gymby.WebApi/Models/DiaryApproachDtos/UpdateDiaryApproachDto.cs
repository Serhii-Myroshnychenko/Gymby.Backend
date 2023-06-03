namespace Gymby.WebApi.Models.DiaryApproachDtos;

public class UpdateDiaryApproachDto
{
    public string ApproachId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public bool IsDone { get; set; }
}
