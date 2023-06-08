using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.DiaryAccesses.Queries.GetAllAvailableDiaries;

public class GetAllAvailableDiariesHandler 
    : IRequestHandler<GetAllAvailableDiariesQuery, List<AvailableDiariesVm>>
{    
    private readonly IApplicationDbContext _dbContext;
        
    public GetAllAvailableDiariesHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<List<AvailableDiariesVm>> Handle(GetAllAvailableDiariesQuery request, CancellationToken cancellationToken)
    {
        var diariesAccesses = await _dbContext.DiaryAccess
            .Where(d => d.UserId == request.UserId && d.Type == AccessType.Shared)
            .Select(d => d.DiaryId)
            .ToListAsync(cancellationToken);

        var diaries = await _dbContext.Diaries
            .Where(d => diariesAccesses.Contains(d.Id))
            .ToListAsync(cancellationToken);

        var result = new List<AvailableDiariesVm>();

        foreach (var diary in diaries)
        {
            result.Add(new AvailableDiariesVm()
            {
                DiaryId = diary.Id,
                Name = diary.Name
            });
        }

        return result;
    }
}
