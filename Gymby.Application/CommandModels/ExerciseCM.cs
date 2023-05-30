namespace Gymby.Application.CommandModels;

public class ExerciseCM
{
    public string Name { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public List<ApproachCM>? Approaches { get; set; }
}
