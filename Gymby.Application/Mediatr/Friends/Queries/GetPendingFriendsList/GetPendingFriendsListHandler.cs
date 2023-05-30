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
    private readonly IFileService _fileService;

    public GetPendingFriendsListHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<ProfileVm>> Handle(GetPendingFriendsListQuery request, CancellationToken cancellationToken)
    {

        var list = await _dbContext.Friends.Where(c => c.ReceiverId == request.UserId && c.Status == Status.Pending).ToListAsync(cancellationToken);

        var profiles = list.Select(f => f.SenderId).ToList();

        var friendsProfiles = await _dbContext.Profiles.Where(p => profiles.Contains(p.UserId)).ToListAsync(cancellationToken);

        for (int i = 0; i < friendsProfiles.Count; i++)
        {
            if (friendsProfiles[i].PhotoAvatarPath != null)
            {
                friendsProfiles[i].PhotoAvatarPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, request.UserId, request.Options.Value.Avatar, friendsProfiles[i].PhotoAvatarPath!);
            }
        }

        return _mapper.Map<List<ProfileVm>>(friendsProfiles);
    }
}
