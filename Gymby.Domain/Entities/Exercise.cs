namespace Gymby.Domain.Entities;
public class Exercise
{
    public string Id { get; set; } = null!;
    public string? DiaryDayId { get; set; }
    public string ExercisePrototypeId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime? Date { get; set; }
    public string Name { get; set; } = null!;
    public DiaryDay? DiaryDay { get; set; }
    public ExercisePrototype ExercisePrototype { get; set; } = null!;
    public ProgramDay? ProgramDay { get; set; }
    public ICollection<Approach> Approaches { get; set; } = null!;
}
