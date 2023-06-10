using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

public class QueryProfileHandler 
    : IRequestHandler<QueryProfileQuery, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IFileService _fileService;

    public QueryProfileHandler(IApplicationDbContext dbContext,IMapper mapper, IMediator mediator, IFileService fileService) =>
        (_dbContext, _mapper, _mediator, _fileService) = (dbContext, mapper, mediator, fileService);

    public async Task<List<ProfileVm>> Handle(QueryProfileQuery request, CancellationToken cancellationToken)
    {
        var friends = await _mediator.Send(new GetMyFriendsListQuery(request.Options) { UserId = request.UserId, Options = request.Options},cancellationToken);
        
        var ownProfile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == request.UserId, cancellationToken);

        var friendsIds = friends.Select(c => c.ProfileId).ToList();

        friendsIds.Add(ownProfile!.Id);

        var profiles = await _dbContext.Profiles
            .Where(p => !friendsIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

        if (request.Type == "trainers")
        {
            profiles = profiles.Where(p => p.IsCoach == true).ToList();
        }

        if (request.Query != null)
        {
            profiles = profiles.Where(p => p.Username!.Contains(request.Query)).ToList();
        }

        var result =  _mapper.Map<List<ProfileVm>>(profiles);

        foreach(var profileVm in result)
        {
            if (profileVm.PhotoAvatarPath != null)
            {
                profileVm.PhotoAvatarPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, profileVm.UserId, request.Options.Value.Avatar, profileVm.PhotoAvatarPath);
            }
            profileVm.Photos = new List<PhotoVm>();
        }

        return result;
    }
}
