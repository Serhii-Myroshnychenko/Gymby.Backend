using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;

public class GetMyMeasurementsHandler
    : IRequestHandler<GetMyMeasurementsQuery, MeasurementsList>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMyMeasurementsHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext,_mapper) = (dbContext,mapper);

    public async Task<MeasurementsList> Handle(GetMyMeasurementsQuery request, CancellationToken cancellationToken)
    {
        var measurements = await _dbContext.Measurements
            .Where(m => m.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == request.UserId && p.IsMeasurement == true)
            .ToListAsync(cancellationToken);

        if (photos.Any())
        {
            for(int i = 0; i < photos.Count; i++)
            {
                photos[i].PhotoPath = Path.Combine(Path.Combine(request.Options.Value.Host, request.Options.Value.Measurement),
                    Path.Combine(photos[i].UserId, photos[i].PhotoPath));
            }
        }

        return new MeasurementsList()
        {
            Measurements = _mapper.Map<List<MeasurementVm>>(measurements),
            Photos = _mapper.Map<List<PhotoVm>>(photos),
        };
    }
}
