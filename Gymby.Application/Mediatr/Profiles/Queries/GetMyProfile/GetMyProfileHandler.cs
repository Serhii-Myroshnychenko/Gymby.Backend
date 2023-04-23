using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Gymby.Application.Common.Exceptions;

namespace Gymby.Application.Mediatr.Profiles.Queries.GetMyProfile;

public class GetMyProfileHandler
    : IRequestHandler<GetMyProfileQuery, ProfileVm>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMyProfileHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);
    
    public async Task<ProfileVm> Handle(GetMyProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == query.UserId, cancellationToken);

        if(profile == null)
        {
            throw new NotFoundEntityException(query.UserId, nameof(Domain.Entities.Profile));
        }
        
        return _mapper.Map<ProfileVm>(profile);
    }
}
