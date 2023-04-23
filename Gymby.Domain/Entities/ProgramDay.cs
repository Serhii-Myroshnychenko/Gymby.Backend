namespace Gymby.Domain.Entities;
public class ProgramDay
{
    public string Id { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Program Program { get; set; } = null!;
    public ICollection<Exercise>? Exercises { get; set; }
    public ICollection<DiaryDay>? DiaryDays { get; set; }
}
