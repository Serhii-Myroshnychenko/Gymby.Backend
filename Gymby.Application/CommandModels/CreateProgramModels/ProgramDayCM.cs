using Gymby.Application.Common.Mappings;

namespace Gymby.Application.CommandModels.CreateProgramModels;

public class ProgramDayCM
{
    public string Name { get; set; } = null!;
    public List<ExerciseCM>? Exercises { get; set; }
}
