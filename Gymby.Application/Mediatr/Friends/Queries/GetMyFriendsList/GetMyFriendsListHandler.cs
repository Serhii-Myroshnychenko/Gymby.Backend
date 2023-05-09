using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Friends.Queries.GetMyFriendsList;

public class GetMyFriendsListHandler 
    : IRequestHandler<GetMyFriendsListQuery, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMyFriendsListHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProfileVm>> Handle(GetMyFriendsListQuery request, CancellationToken cancellationToken)
    {
        var friendProfiles = await _dbContext.Friends
            .Where(f => (f.SenderId == request.UserId || f.ReceiverId == request.UserId) && f.Status == Status.Confirmed)
            .Join(_dbContext.Profiles, f => f.SenderId == request.UserId ? f.ReceiverId : f.SenderId, p => p.UserId, (f, p) => p)
            .ToListAsync(cancellationToken);

        var photos = await _dbContext.Photos.Where(p => p.UserId == request.UserId && p.IsMeasurement == false).ToListAsync(cancellationToken);

        for(int i = 0; i < friendProfiles.Count; i++)
        {
            if (friendProfiles[i].PhotoAvatarPath != null)
            {
                friendProfiles[i].PhotoAvatarPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
                    Path.Combine(friendProfiles[i].UserId, friendProfiles[i].PhotoAvatarPath!));
            }
        }

        var result = _mapper.Map<List<ProfileVm>>(friendProfiles);

        for (int i = 0; i < result.Count; i++)
        {
            var currentFriendPhotos = photos.Where(p => p.UserId == result[i].UserId).ToList();
            if (currentFriendPhotos != null && currentFriendPhotos.Count > 0)
            {
                result[i].Photos = currentFriendPhotos.Select(c => c.PhotoPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
                    Path.Combine(result[i].UserId, c.PhotoPath))).ToList();
            }
        }

        return result;
    }
}
