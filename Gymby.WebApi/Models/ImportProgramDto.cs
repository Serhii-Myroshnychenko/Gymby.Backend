namespace Gymby.WebApi.Models;

public class ImportProgramDto
{
    public string? DiaryId { get; set; }
    public string ProgramId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public List<string> DaysOfWeek { get; set; } = null!;
}
