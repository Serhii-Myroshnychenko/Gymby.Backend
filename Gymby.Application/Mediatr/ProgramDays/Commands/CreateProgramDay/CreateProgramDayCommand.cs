using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;

public class CreateProgramDayCommand : IRequest<ProgramDayVm>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
