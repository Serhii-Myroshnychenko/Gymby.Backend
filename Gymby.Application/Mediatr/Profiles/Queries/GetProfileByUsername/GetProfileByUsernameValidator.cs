using FluentValidation;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

public class GetProfileByUsernameValidator : AbstractValidator<GetProfileByUsernameQuery>
{
    public GetProfileByUsernameValidator()
    {
        RuleFor(c => c.Username).NotEmpty().NotNull();
    }
}
