using MediatR;

namespace Gymby.Application.Mediatr.ProgramDays.Commands.DeleteProgramDay;

public class DeleteProgramDayCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ProgramDayId { get; set; } = null!;
    public string ProgramId { get; set;} = null!;
}
