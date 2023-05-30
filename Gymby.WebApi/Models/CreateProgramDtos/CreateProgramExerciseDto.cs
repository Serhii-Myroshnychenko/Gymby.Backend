namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramExerciseDto
{
    public string Name { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public List<CreateProgramApproacheDto>? Approaches { get; set; }
}
