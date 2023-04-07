using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class ExercisePrototype
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Category Category { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = null!;
}
