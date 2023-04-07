namespace Gymby.Domain.Entities;
public class DiaryDay
{
    public int Id { get; set; }
    public int DiaryId { get; set; }
    public int? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public Diary Diary { get; set; } = null!;
    public ProgramDay? ProgramDay { get; set; }
}
