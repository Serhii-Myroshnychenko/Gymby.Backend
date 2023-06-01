namespace Gymby.Domain.Entities;
public class ProgramDay
{
    public string Id { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public virtual Program Program { get; set; } = null!;
    public virtual ICollection<Exercise>? Exercises { get; set; }
    public virtual ICollection<DiaryDay>? DiaryDays { get; set; }
}
