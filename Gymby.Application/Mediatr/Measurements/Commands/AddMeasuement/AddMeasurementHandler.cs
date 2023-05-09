using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using MediatR;

namespace Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;

public class AddMeasurementHandler
    : IRequestHandler<AddMeasurementCommand, string>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddMeasurementHandler(IApplicationDbContext dbContext, IMapper mapper) =>
        (_dbContext, _mapper) = (dbContext, mapper);   

    public async Task<string> Handle(AddMeasurementCommand request, CancellationToken cancellationToken)
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

        return measurement.Id;
    }
}
