using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

public class QueryProfileHandler 
    : IRequestHandler<QueryProfileQuery, List<ProfileVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public QueryProfileHandler(IApplicationDbContext dbContext,IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<ProfileVm>> Handle(QueryProfileQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Profiles.AsQueryable();

        if (request.Type == "trainers")
        {
            query = query.Where(p => p.IsCoach == true);
        }

        if (request.Query != null)
        {
            query = query.Where(p => p.Username!.Contains(request.Query));
        }

        var profiles = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<ProfileVm>>(profiles);
    }
}
