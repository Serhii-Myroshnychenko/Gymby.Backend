namespace Gymby.Application.ViewModels;

public class DiaryCalendarRepresentationVm
{
    public string DiaryDayId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public List<string> Categories { get; set; } = new List<string>();
}
