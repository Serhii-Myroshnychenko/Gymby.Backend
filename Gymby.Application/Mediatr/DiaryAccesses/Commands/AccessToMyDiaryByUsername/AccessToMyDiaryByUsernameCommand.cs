using MediatR;

namespace Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;

public class AccessToMyDiaryByUsernameCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
}
