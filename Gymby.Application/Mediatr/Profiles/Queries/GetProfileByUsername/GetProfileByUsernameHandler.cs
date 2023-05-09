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

    public GetProfileByUsernameHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<ProfileVm> Handle(GetProfileByUsernameQuery request, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken);

        if(profile == null)
        {
            throw new NotFoundEntityException(request.Username, nameof(Domain.Entities.Profile));
        }

        var photos = await _dbContext.Photos.Where(p => p.UserId == profile.UserId).ToListAsync();
        var result = _mapper.Map<ProfileVm>(profile);

        if (result.PhotoAvatarPath != null)
        {
            result.PhotoAvatarPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
            Path.Combine(result.ProfileId, result.PhotoAvatarPath));
        }
        if(photos != null)
        {
            result.Photos = photos.Select(c => c.PhotoPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Profile),
                Path.Combine(profile.UserId, c.PhotoPath))).ToList();
        }

        return result;
    }
}
