using Gymby.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Gymby.Application.Common.Exceptions;
using Gymby.Domain.Enums;
using Gymby.Application.ViewModels;

using AutoMapper;

namespace Gymby.Application.Mediatr.Friends.Commands.AcceptFriendship;

public class AcceptFriendshipHandler
    : IRequestHandler<AcceptFriendshipCommand, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AcceptFriendshipHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<ProfileVm>> Handle(AcceptFriendshipCommand command, 
        CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .Where(p => p.Username == command.Username)
            .FirstOrDefaultAsync(cancellationToken);

        if (profile == null)
        {
            throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Profile));
        }

        var friendship = await _dbContext.Friends
            .Where(f => f.SenderId == profile.UserId && f.ReceiverId == command.UserId && f.Status == Status.Pending)
            .FirstOrDefaultAsync(cancellationToken);

        if(friendship == null)
        {
            throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Friend));
        }

        friendship.Status = Status.Confirmed;

        await _dbContext.SaveChangesAsync(cancellationToken);

        var list = await _dbContext.Friends.Where(c => c.ReceiverId == command.UserId && c.Status == Status.Pending).ToListAsync(cancellationToken);

        var profiles = list.Select(f => f.SenderId).ToList();

        List<Domain.Entities.Profile> friendsProfiles = await _dbContext.Profiles
            .Where(p => profiles.Contains(p.UserId))
            .ToListAsync(cancellationToken);

        for (int i = 0; i < friendsProfiles.Count; i++)
        {
            if (friendsProfiles[i].PhotoAvatarPath != null)
            {
                friendsProfiles[i].PhotoAvatarPath = await _fileService.GetPhotoAsync(command.Options.Value.ContainerName, friendsProfiles[i].UserId, command.Options.Value.Avatar, friendsProfiles[i].PhotoAvatarPath!);
            }
        }
        var result = _mapper.Map<List<ProfileVm>>(friendsProfiles);
        
        foreach (var profileVm in result)
        {
            profileVm.Photos = new List<PhotoVm>();
        }

        return result;
    }
}
