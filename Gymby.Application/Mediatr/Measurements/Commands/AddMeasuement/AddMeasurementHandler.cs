using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;

public class AddMeasurementHandler
    : IRequestHandler<AddMeasurementCommand, MeasurementsList>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AddMeasurementHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<MeasurementsList> Handle(AddMeasurementCommand request, CancellationToken cancellationToken)
    {
        var measurement = new Measurement()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            Date = request.Date,
            Type = request.Type,
            Value = request.Value,
            Unit = request.Unit
        };

        await _dbContext.Measurements.AddAsync(measurement,cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var measurements = await _dbContext.Measurements
            .Where(m => m.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == request.UserId && p.IsMeasurement == true)
            .ToListAsync(cancellationToken);

        if (photos.Any())
        {
            for (int i = 0; i < photos.Count; i++)
            {
                photos[i].PhotoPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, request.UserId, request.Options.Value.Measurement, photos[i].PhotoPath);
            }
        }

        measurements = measurements.OrderBy(m => m.Date).ToList();

        return new MeasurementsList()
        {
            Measurements = _mapper.Map<List<MeasurementVm>>(measurements),
            Photos = _mapper.Map<List<PhotoVm>>(photos),
        };
    }
}
