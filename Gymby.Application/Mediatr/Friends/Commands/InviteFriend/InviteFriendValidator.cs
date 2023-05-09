using FluentValidation;
namespace Gymby.Application.Mediatr.Friends.Commands.InviteFriend;

public class InviteFriendValidator : AbstractValidator<InviteFriendCommand>
{
    public InviteFriendValidator()
    {
        RuleFor(c => c.UserId).NotNull().NotEmpty();
        RuleFor(c => c.Username).NotNull().NotEmpty();
    }
}
