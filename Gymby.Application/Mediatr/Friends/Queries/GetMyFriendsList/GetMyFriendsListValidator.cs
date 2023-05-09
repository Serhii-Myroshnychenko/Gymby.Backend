using FluentValidation;

namespace Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;

public class GetMyFriendsListValidator : AbstractValidator<GetMyFriendsListQuery>
{
    public GetMyFriendsListValidator()
    {
        RuleFor(c => c.UserId).NotNull().NotEmpty();
    }
}

