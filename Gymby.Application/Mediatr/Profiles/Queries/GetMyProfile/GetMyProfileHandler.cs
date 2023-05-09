using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Utils;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileHandler
    : IRequestHandler<GetMyProfileQuery, ProfileVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMyProfileHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);
    
    // Пофиксить логику фоток   
    
    public async Task<ProfileVm> Handle(GetMyProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == query.UserId, cancellationToken);

        var photos = await _dbContext.Photos.Where(p => p.UserId == query.UserId && p.IsMeasurement == false).ToListAsync(cancellationToken);

        if(profile == null)
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
            result.PhotoAvatarPath = Path.Combine(Path.Combine(query.Options.Value.Host, query.Options.Value.Profile),
                Path.Combine(query.UserId, result.PhotoAvatarPath));
        }

        if(photos != null)
        {
            result.Photos = photos.Select(c => c.PhotoPath = Path.Combine(Path.Combine(query.Options.Value.Host, query.Options.Value.Profile),
                Path.Combine(query.UserId, c.PhotoPath))).ToList();
        }

        return result;
    }
}
