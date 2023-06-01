using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Measurements.Commands.EditMeasurement;

public class EditMeasurementHandler 
    : IRequestHandler<EditMeasurementCommand, MeasurementsList>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public EditMeasurementHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<MeasurementsList> Handle(EditMeasurementCommand request, CancellationToken cancellationToken)
    {
        var measurement = await _dbContext.Measurements
            .FirstOrDefaultAsync(m => m.Id == request.Id && m.UserId == request.UserId, cancellationToken)
            ?? throw new InsufficientRightsException($"The measurement ({request.Id}) was not found or you({request.UserId}) dont have any permissions to edit it");
        
        measurement.Unit = request.Unit;
        measurement.Value = request.Value;
        measurement.Date = request.Date;
        measurement.Type = request.Type;

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

        return new MeasurementsList()
        {
            Measurements = _mapper.Map<List<MeasurementVm>>(measurements),
            Photos = _mapper.Map<List<PhotoVm>>(photos),
        };
    }
}
