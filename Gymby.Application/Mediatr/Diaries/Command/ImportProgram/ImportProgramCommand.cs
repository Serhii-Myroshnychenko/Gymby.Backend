using MediatR;

namespace Gymby.Application.Mediatr.Diaries.Command.ImportProgram;

public class ImportProgramCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string? DiaryId { get; set; }
    public string ProgramId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public List<string> DaysOfWeek { get; set; } = null!;
}
