using FluentValidation;

namespace Gymby.Application.Mediatr.Friends.Commands.RejectFriendship;

public class RejectFriendshipValidator : AbstractValidator<RejectFriendshipCommand>
{
    public RejectFriendshipValidator()
    {
        RuleFor(c => c.UserId).NotNull().NotEmpty();
        RuleFor(c => c.Username).NotNull().NotEmpty();
    }
}
