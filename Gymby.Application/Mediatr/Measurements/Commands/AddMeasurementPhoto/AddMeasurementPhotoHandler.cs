using Gymby.Application.Interfaces;
using Gymby.Domain.Entities;
using MediatR;

namespace Gymby.Application.Mediatr.Measurements.Commands.AddMeasurementPhoto;

public class AddMeasurementPhotoHandler
    : IRequestHandler<AddMeasurementPhotoCommand, string>
{
    private readonly IApplicationDbContext _dbContext;

    public AddMeasurementPhotoHandler(IApplicationDbContext dbContext) =>
        (_dbContext) = (dbContext);

    public async Task<string> Handle(AddMeasurementPhotoCommand request, CancellationToken cancellationToken)
    {
        var path = Path.Combine(Path.Combine(request.Options.Value.Path!, request.Options.Value.Measurement), request.UserId);

        using (var fileStream = new FileStream(Path.Combine(path, request.File.FileName), FileMode.Create))
        {
              await request.File.CopyToAsync(fileStream, cancellationToken);
        }

        var photo = new Photo()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            IsMeasurement = true,
            PhotoPath = request.File.Name,
            MeasurementDate = request.MeasurementDate,
            CreationDate = DateTime.Now,
        };

        await _dbContext.Photos.AddAsync(photo,cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return photo.Id;
    }
}
