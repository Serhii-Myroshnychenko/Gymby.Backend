namespace Gymby.Domain.Entities;
public class Exercise
{
    public int Id { get; set; }
    public int? DiaryDayId { get; set; }
    public int ExercisePrototypeId { get; set; }
    public int? ProgramDayId { get; set; }
    public DateTime? Date { get; set; }
    public string Name { get; set; } = null!;
    public DiaryDay? DiaryDay { get; set; }
    public ExercisePrototype ExercisePrototype { get; set; } = null!;
    public ProgramDay? ProgramDay { get; set; }
    public ICollection<Approach> Approaches { get; set; } = null!;
}
