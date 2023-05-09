using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Friends.Queries.GetPendingFriendsList;

public class GetPendingFriendsListHandler
    : IRequestHandler<GetPendingFriendsListQuery, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPendingFriendsListHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProfileVm>> Handle(GetPendingFriendsListQuery request, CancellationToken cancellationToken)
    {

        var list = await _dbContext.Friends.Where(c => c.ReceiverId == request.UserId && c.Status == Status.Pending).ToListAsync(cancellationToken);
        var profiles = list.Select(f => f.SenderId).ToList();
        var friendsProfiles = await _dbContext.Profiles.Where(p => profiles.Contains(p.UserId)).ToListAsync(cancellationToken);
        var friendsPhotos = await _dbContext.Photos.Where(p => profiles.Contains(p.UserId) && p.IsMeasurement == false).ToListAsync(cancellationToken);

        for (int i = 0; i < friendsProfiles.Count; i++)
        {
            if (friendsProfiles[i].PhotoAvatarPath != null)
            {
                friendsProfiles[i].PhotoAvatarPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
                    Path.Combine(friendsProfiles[i].UserId, friendsProfiles[i].PhotoAvatarPath!));
            }
        }

        var result = _mapper.Map<List<ProfileVm>>(friendsProfiles);

        for (int i = 0; i < result.Count; i++)
        {
            var currentFriendPhotos = friendsPhotos.Where(p => p.UserId == result[i].UserId).ToList();
            if (currentFriendPhotos != null && currentFriendPhotos.Count > 0)
            {
                result[i].Photos = currentFriendPhotos.Select(c => c.PhotoPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
                    Path.Combine(result[i].UserId, c.PhotoPath))).ToList();
            }
        }

        return result;
    }
}
