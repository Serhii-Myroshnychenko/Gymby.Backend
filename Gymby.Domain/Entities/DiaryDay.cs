namespace Gymby.Domain.Entities;
public class DiaryDay
{
    public string Id { get; set; } = null!;
    public string DiaryId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public Diary Diary { get; set; } = null!;
    public ProgramDay? ProgramDay { get; set; }
    public ICollection<Exercise>? Exercises { get; set; }
}
