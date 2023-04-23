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

    public UpdateProfileHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ProfileVm> Handle(UpdateProfileCommand updateProfile,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.Username == updateProfile.Username, cancellationToken);

        if(entity == null)
        {
            throw new NotFoundEntityException(updateProfile.Username!, nameof(Domain.Entities.Profile));
        }

        if(updateProfile.Avatar != null)
        {
            var path = Path.Combine(updateProfile.Path!, updateProfile.ProfileId);

            DirectoryInfo directory = new (path);

            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                await Task.Run(() => file.Delete(), cancellationToken);
            }

            using (var fileStream = new FileStream(Path.Combine(path,updateProfile.Avatar.FileName), FileMode.Create))
            {
                await updateProfile.Avatar.CopyToAsync(fileStream, cancellationToken);
            }

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

        return _mapper.Map<ProfileVm>(entity);
    }
}
