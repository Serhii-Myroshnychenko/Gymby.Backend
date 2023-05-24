using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;

public class DeleteMeasurementHandler
    : IRequestHandler<DeleteMeasurementCommand, MeasurementsList>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public DeleteMeasurementHandler(IApplicationDbContext dbContext, IFileService fileService, IMapper mapper) =>
        (_dbContext, _fileService, _mapper) = (dbContext, fileService, mapper);

    public async Task<MeasurementsList> Handle(DeleteMeasurementCommand request, CancellationToken cancellationToken)
    {
        var measurement = await _dbContext.Measurements
            .Where(m => m.UserId == request.UserId && m.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(measurement == null)      
        {
            throw new NotFoundEntityException(request.Id!, nameof(Measurement));
        }

        _dbContext.Measurements.Remove(measurement);
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
