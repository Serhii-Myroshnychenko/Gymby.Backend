using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;

public class DeleteMeasurementHandler
    : IRequestHandler<DeleteMeasurementCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteMeasurementHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<string> Handle(DeleteMeasurementCommand request, CancellationToken cancellationToken)
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

        return measurement.Id;
    }
}
