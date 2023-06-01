using Gymby.Domain.Enums;

namespace Gymby.Domain.Entities;
public class ExercisePrototype
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Category Category { get; set; }
    public virtual ICollection<Exercise> Exercises { get; set; } = null!;
}
