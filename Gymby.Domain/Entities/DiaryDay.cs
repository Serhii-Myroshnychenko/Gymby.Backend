namespace Gymby.Domain.Entities;
public class DiaryDay
{
    public string Id { get; set; } = null!;
    public string DiaryId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public virtual Diary Diary { get; set; } = null!;
    public virtual ProgramDay? ProgramDay { get; set; }
    public virtual ICollection<Exercise>? Exercises { get; set; }
}
