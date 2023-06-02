using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.GetPersonalPrograms;

public class GetPersonalProgramsQuery : IRequest<List<ProgramVm>>
{
    public string UserId { get; set; } = null!;
}
