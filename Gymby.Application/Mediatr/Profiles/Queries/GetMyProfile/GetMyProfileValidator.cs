using FluentValidation;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileValidator : AbstractValidator<GetMyProfileQuery>
{
    public GetMyProfileValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().NotNull();
    }
}
