namespace Gymby.Domain.Entities;
public class Exercise
{
    public int Id { get; set; }
    public int? DiaryDayId { get; set; }
    public int ExercisePrototypeId { get; set; }
    public int? ProgramDayId { get; set; }
    public DateTime? Date { get; set; }
    public string Name { get; set; } = null!;
}
