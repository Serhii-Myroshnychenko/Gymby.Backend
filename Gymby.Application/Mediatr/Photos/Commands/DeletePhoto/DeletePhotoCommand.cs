using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;

public class DeletePhotoCommand
    : IRequest<List<PhotoVm>>
{
    public string UserId { get; set; } = null!;
    public string PhotoId { get; set; } = null!;
    public string Type { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; }

    public DeletePhotoCommand(IOptions<AppConfig> options, string type)
    {
        Options = options;
        Type = type;
    }
}
