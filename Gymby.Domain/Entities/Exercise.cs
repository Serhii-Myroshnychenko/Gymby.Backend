namespace Gymby.Domain.Entities;
public class Exercise
{
    public string Id { get; set; } = null!;
    public string? DiaryDayId { get; set; }
    public string ExercisePrototypeId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime? Date { get; set; }
    public string Name { get; set; } = null!;
    public virtual DiaryDay? DiaryDay { get; set; }
    public virtual ExercisePrototype ExercisePrototype { get; set; } = null!;
    public virtual ProgramDay? ProgramDay { get; set; }
    public virtual ICollection<Approach> Approaches { get; set; } = null!;
}
