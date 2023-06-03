using MediatR;

namespace Gymby.Application.Mediatr.Diaries.Command.CreateDiary;

public class CreateDiaryCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
}
