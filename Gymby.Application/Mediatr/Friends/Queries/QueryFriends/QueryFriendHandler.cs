using AutoMapper;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Friends.Queries.QueryFriends;

public class QueryFriendHandler 
    : IRequestHandler<QueryFriendQuery, List<ProfileVm>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public QueryFriendHandler(IMapper mapper, IMediator mediator) =>
        (_mapper, _mediator) = (mapper, mediator);

    public async Task<List<ProfileVm>> Handle(QueryFriendQuery request, CancellationToken cancellationToken)
    {
        var friends = await _mediator.Send(new GetMyFriendsListQuery(request.Options) { UserId = request.UserId, Options = request.Options},cancellationToken);

        if (request.Type == "trainers")
        {
            friends = friends.Where(p => p.IsCoach == true).ToList();
        }

        if (request.Query != null)
        {
            friends = friends.Where(p => p.Username!.Contains(request.Query)).ToList();
        }

        return _mapper.Map<List<ProfileVm>>(friends);
    }
}
