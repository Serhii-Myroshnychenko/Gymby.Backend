using MediatR;

namespace Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;

public class AccessProgramToUserByUsernameQuery : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string ProgramId { get; set;} = null!;
    public string Username { get; set; } = null!;
}
