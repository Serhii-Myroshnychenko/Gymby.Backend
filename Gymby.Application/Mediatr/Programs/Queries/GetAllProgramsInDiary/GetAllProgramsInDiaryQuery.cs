using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.GetAllProgramsInDiary;

public class GetAllProgramsInDiaryQuery : IRequest<List<ProgramVm>>
{
    public string UserId { get; set; } = null!;
}
