using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Friends.Commands.DeleteFriend;

public class DeleteFriendHandler
    : IRequestHandler<DeleteFriendCommand, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public DeleteFriendHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<ProfileVm>> Handle(DeleteFriendCommand command,
        CancellationToken cancellationToken)
    {
        var friendProfile = await _dbContext.Profiles
            .FirstOrDefaultAsync(c => c.Username == command.Username, cancellationToken)
            ?? throw new NotFoundEntityException(command.Username, nameof(Domain.Entities.Profile));

        var friendship = await _dbContext.Friends
            .Where(f => (f.SenderId == friendProfile.UserId && f.ReceiverId == command.UserId && f.Status == Status.Confirmed) ||
             (f.SenderId == command.UserId && f.ReceiverId == friendProfile.UserId && f.Status == Status.Confirmed)).FirstOrDefaultAsync(cancellationToken)
             ?? throw new NotFoundEntityException(command.Username, nameof(Friend));

        _dbContext.Friends.Remove(friendship);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var friendProfiles = await _dbContext.Friends
            .Where(f => (f.SenderId == command.UserId || f.ReceiverId == command.UserId) && f.Status == Status.Confirmed)
            .Join(_dbContext.Profiles, f => f.SenderId == command.UserId ? f.ReceiverId : f.SenderId, p => p.UserId, (f, p) => p)
            .ToListAsync(cancellationToken);

        for (int i = 0; i < friendProfiles.Count; i++)
        {
            if (friendProfiles[i].PhotoAvatarPath != null)
            {
                friendProfiles[i].PhotoAvatarPath = await _fileService.GetPhotoAsync(command.Options.Value.ContainerName, friendProfiles[i].UserId, command.Options.Value.Avatar, friendProfiles[i].PhotoAvatarPath!);
            }
        }

        var result =  _mapper.Map<List<ProfileVm>>(friendProfiles);

        foreach(var profileVm in result)
        {
            profileVm.Photos = new List<PhotoVm>();
        }

        return result;
    }
}
