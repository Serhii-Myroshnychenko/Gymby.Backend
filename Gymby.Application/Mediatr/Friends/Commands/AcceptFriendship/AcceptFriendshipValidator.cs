using FluentValidation;

namespace Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

public class AcceptFriendshipValidator : AbstractValidator<AcceptFriendshipCommand>
{
    public AcceptFriendshipValidator()
    {
        RuleFor(c => c.UserId).NotNull().NotEmpty();
        RuleFor(c => c.Username).NotNull().NotEmpty();
    }
}
