namespace Gymby.WebApi.Models.ProgramExerciseDto;

public class CreateProgramsExerciseDto
{
    public string ProgramId { get; set; } = null!;
    public string ExercisePrototypeId { get; set;} = null!;
    public string ProgramDayId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
