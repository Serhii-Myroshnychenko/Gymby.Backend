namespace Gymby.WebApi.Models.ProgramExerciseDto;

public class UpdateProgramExerciseDto
{
    public string ProgramId { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
