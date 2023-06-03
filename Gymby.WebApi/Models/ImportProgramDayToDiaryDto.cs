namespace Gymby.WebApi.Models;

public class ImportProgramDayToDiaryDto
{
    public string? DiaryId { get; set; }
    public string ProgramId { get; set; } = null!;
    public string ProgramDayId { get; set; } = null!;
    public DateTime Date { get; set; }
}
