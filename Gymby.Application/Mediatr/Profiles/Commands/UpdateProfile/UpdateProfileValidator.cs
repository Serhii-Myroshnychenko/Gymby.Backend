using FluentValidation;

namespace Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;

public class UpdateProfileValidator 
    : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(c => c.ProfileId).NotEqual(string.Empty).NotNull();
        RuleFor(c => c.UserId).NotEqual(string.Empty).NotNull();
        RuleFor(c => c.Email).NotNull().NotEmpty();
    }
}
