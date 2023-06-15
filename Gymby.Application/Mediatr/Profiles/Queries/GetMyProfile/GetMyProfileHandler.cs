using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Gymby.Application.Utils;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileHandler
    : IRequestHandler<GetMyProfileQuery, ProfileVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public GetMyProfileHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);
    
    public async Task<ProfileVm> Handle(GetMyProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == query.UserId, cancellationToken);

        var photos = await _dbContext.Photos.Where(p => p.UserId == query.UserId && p.IsMeasurement == false).ToListAsync(cancellationToken);

        if (profile == null)
        {
            var username = UsernameHandler.GenerateUsername();
            var existingUsernames = new HashSet<string>(_dbContext.Profiles.Select(p => p.Username)!);

            while (existingUsernames.Contains(username))
            {
                username = UsernameHandler.GenerateUsername();
            }

            var newProfile = new Domain.Entities.Profile()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = query.UserId,
                Email = query.Email,
                Username = username,
                IsCoach = false

            };

            await _dbContext.Profiles.AddAsync(newProfile, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            profile = newProfile;
        }

        var result = _mapper.Map<ProfileVm>(profile);

        if(result.PhotoAvatarPath != null)
        {
            result.PhotoAvatarPath = await _fileService.GetPhotoAsync(query.Options.Value.ContainerName, query.UserId, query.Options.Value.Avatar, result.PhotoAvatarPath);
        }

        if(photos.Any())
        {
            result.Photos = _mapper.Map<List<PhotoVm>>(photos);

            foreach(var elem in result.Photos)
            {
                elem.PhotoPath = await _fileService.GetPhotoAsync(query.Options.Value.ContainerName, query.UserId, query.Options.Value.Profile, elem.PhotoPath);
            }   
        }
        else
        {
            result.Photos = new List<PhotoVm>();
        }

        return result;
    }
}
