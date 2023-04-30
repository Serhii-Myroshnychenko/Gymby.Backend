using FluentValidation;

namespace Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

public class CreateProfileValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileValidator()
    {
        RuleFor(c => c.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(c => c.UserId).NotNull().NotEmpty();
    }
}
