using FluentValidation; 
namespace Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;

public class GetPendingFriendsListValidator : AbstractValidator<GetPendingFriendsListQuery>
{
    public GetPendingFriendsListValidator()
    {
        RuleFor(c => c.UserId).NotNull().NotEmpty();
    }
}
