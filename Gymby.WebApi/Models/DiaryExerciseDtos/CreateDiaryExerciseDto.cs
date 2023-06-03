namespace Gymby.WebApi.Models.DiaryExerciseDtos;

public class CreateDiaryExerciseDto
{
    public string DiaryId { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; } = null!;
}
