using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetProfileByUsername;

public class GetProfileByUsernameHandler
    : IRequestHandler<GetProfileByUsernameQuery, ProfileVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public GetProfileByUsernameHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<ProfileVm> Handle(GetProfileByUsernameQuery request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken);

        if(profile == null)
        {
            throw new NotFoundEntityException(request.Username, nameof(Domain.Entities.Profile));
        }

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == profile.UserId)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<ProfileVm>(profile);

        if (result.PhotoAvatarPath != null)
        {
            result.PhotoAvatarPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, profile.UserId, request.Options.Value.Avatar, result.PhotoAvatarPath);
        }
        if(photos.Any())
        {
            result.Photos = await _fileService.GetListOfPhotos(request.Options.Value.ContainerName, profile.UserId, request.Options.Value.Profile);
        }

        return result;
    }
}
