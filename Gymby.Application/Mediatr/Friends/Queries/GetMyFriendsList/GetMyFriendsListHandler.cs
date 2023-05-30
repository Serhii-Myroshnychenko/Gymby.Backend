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
    private readonly IFileService _fileService;

    public GetMyFriendsListHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<ProfileVm>> Handle(GetMyFriendsListQuery request, CancellationToken cancellationToken)
    {
        var friendProfiles = await _dbContext.Friends
            .Where(f => (f.SenderId == request.UserId || f.ReceiverId == request.UserId) && f.Status == Status.Confirmed)
            .Join(_dbContext.Profiles, f => f.SenderId == request.UserId ? f.ReceiverId : f.SenderId, p => p.UserId, (f, p) => p)
            .ToListAsync(cancellationToken);

        for(int i = 0; i < friendProfiles.Count; i++)
        {
            if (friendProfiles[i].PhotoAvatarPath != null)
            {
                friendProfiles[i].PhotoAvatarPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, friendProfiles[i].UserId, request.Options.Value.Avatar, friendProfiles[i].PhotoAvatarPath!);
            }
        }

        return _mapper.Map<List<ProfileVm>>(friendProfiles);
    }
}
