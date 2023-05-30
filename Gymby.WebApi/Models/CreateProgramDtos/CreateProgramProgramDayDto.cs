namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramProgramDayDto
{
    public string Name { get; set; } = null!;
    public List<CreateProgramExerciseDto>? Exercises { get; set; }
}
