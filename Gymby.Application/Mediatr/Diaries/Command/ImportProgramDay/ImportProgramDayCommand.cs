using MediatR;

namespace Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;

public class ImportProgramDayCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string? DiaryId { get; set; }
    public string ProgramId { get; set; } = null!;
    public string ProgramDayId { get; set;} = null!;
    public DateTime Date { get; set; }
}
