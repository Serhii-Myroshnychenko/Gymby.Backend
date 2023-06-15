using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
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

        if (updateProfile.PhotoAvatarPath != null)
        {
            var newPhotoName = Guid.NewGuid().ToString() + Path.GetExtension(updateProfile.PhotoAvatarPath.FileName);

            if (entity.PhotoAvatarPath != null)
            {
                await _fileService.DeletePhotoAsync(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Avatar);
            }
            await _fileService.AddPhotoAsync(updateProfile.Options.Value.ContainerName, updateProfile.UserId, updateProfile.Options.Value.Avatar, updateProfile.PhotoAvatarPath, newPhotoName);

            entity.PhotoAvatarPath = newPhotoName;
        }

        if(updateProfile.Username != null)
        {
            var entityWithGivenUsername = await _dbContext.Profiles
                .Where(p => p.Username == updateProfile.Username && p.Id != updateProfile.ProfileId)
                .FirstOrDefaultAsync(cancellationToken);
            if (entityWithGivenUsername == null)
            {
                var diaryAccess = await _dbContext.DiaryAccess
                    .FirstOrDefaultAsync(d => d.UserId == entity.UserId && d.Type == AccessType.Owner, cancellationToken)
                        ?? throw new NotFoundEntityException(entity.UserId, nameof(DiaryAccess));

                var diary = await _dbContext.Diaries
                    .FirstOrDefaultAsync(d => d.Id == diaryAccess.DiaryId, cancellationToken)
                        ?? throw new NotFoundEntityException(diaryAccess.DiaryId, nameof(Diary));

                diary.Name = updateProfile.Username + " diary";
                entity.Username = updateProfile.Username;
            }
            else
            {
                throw new InvalidUsernameException("Invalid Username");
            }
        }

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
            result.Photos = _mapper.Map<List<PhotoVm>>(photos);

            foreach (var elem in result.Photos)
            {
                elem.PhotoPath = await _fileService.GetPhotoAsync(updateProfile.Options.Value.ContainerName, elem.UserId, updateProfile.Options.Value.Profile, elem.PhotoPath);
            }
        }
      
        return result;
    }
}
