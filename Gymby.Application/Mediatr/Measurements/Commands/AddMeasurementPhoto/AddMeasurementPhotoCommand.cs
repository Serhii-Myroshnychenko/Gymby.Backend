using Gymby.Application.Config;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Measurements.Commands.AddMeasurementPhoto;

public class AddMeasurementPhotoCommand : IRequest<string>
{
    public string UserId { get; set; } = null!;
    public IFormFile File { get; set; } = null!;
    public DateTime MeasurementDate { get; set; }
    public IOptions<AppConfig> Options { get; set; }

    public AddMeasurementPhotoCommand(IOptions<AppConfig> options) =>
        Options = options;
}
