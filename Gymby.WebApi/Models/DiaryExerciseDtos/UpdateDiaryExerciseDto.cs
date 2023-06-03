namespace Gymby.WebApi.Models.DiaryExerciseDtos;

public class UpdateDiaryExerciseDto
{
    public string ExerciseId { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
