using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;

public class UpdateProfileHandler
    : IRequestHandler<UpdateProfileCommand, ProfileVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public UpdateProfileHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ProfileVm> Handle(UpdateProfileCommand updateProfile,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.UserId == updateProfile.UserId, cancellationToken);

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == updateProfile.UserId && p.IsMeasurement == false)
            .ToListAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundEntityException(updateProfile.Username!, nameof(Domain.Entities.Profile));
        }

        if (updateProfile.Avatar != null)
        {
            if (entity.PhotoAvatarPath != null)
            {
                await _fileService.DeletePhotoAsync(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Avatar);
            }
            await _fileService.AddPhotoAsync(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Avatar, updateProfile.Avatar);

            entity.PhotoAvatarPath = updateProfile.Avatar.FileName;
        }

        entity.Username = updateProfile.Username;
        entity.FirstName = updateProfile.FirstName;
        entity.LastName = updateProfile.LastName;
        entity.Description = updateProfile.Description;
        entity.InstagramUrl = updateProfile.InstagramUrl;
        entity.FacebookUrl = updateProfile.FacebookUrl;
        entity.TelegramUsername = updateProfile.TelegramUsername;
        entity.Email = updateProfile.Email;

        await _dbContext.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<ProfileVm>(entity);

        if (result.PhotoAvatarPath != null)
        {
            result.PhotoAvatarPath = await _fileService.GetPhotoAsync(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Avatar, result.PhotoAvatarPath);
        }

        if (photos.Any())
        {
            result.Photos = await _fileService.GetListOfPhotos(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Profile);
        }
      
        return result;
    }
}
