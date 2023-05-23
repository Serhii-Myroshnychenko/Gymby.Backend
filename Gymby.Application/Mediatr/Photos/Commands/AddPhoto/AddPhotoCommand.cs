using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Photos.Commands.AddPhoto;

public class AddPhotoCommand : IRequest<List<PhotoVm>>
{
    public string UserId { get; set; } = null!;
    public IFormFile Photo { get; set; } = null!;
    public string Type { get; set; } = null!;
    public DateTime? MeasurementDate { get; set; }
    public IOptions<AppConfig> Options { get; set; }

    public AddPhotoCommand(IOptions<AppConfig> options, string type)
    {
        Options = options;
        Type = type;
    }
}
