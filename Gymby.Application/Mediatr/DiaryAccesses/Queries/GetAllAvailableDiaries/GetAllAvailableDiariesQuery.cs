using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.DiaryAccesses.Queries.GetAllAvailableDiaries;

public class GetAllAvailableDiariesQuery : IRequest<List<AvailableDiariesVm>>
{
    public string UserId { get; set; } = null!;
}