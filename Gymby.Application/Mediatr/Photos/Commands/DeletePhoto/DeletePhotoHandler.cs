using AutoMapper;
using Gymby.Application.Common.Exceptions;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;

public class DeletePhotoHandler
    : IRequestHandler<DeletePhotoCommand, List<PhotoVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public DeletePhotoHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<PhotoVm>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var photoType = request.Type == "Profiles" ? request.Options.Value.Profile : request.Options.Value.Measurement;
        var isMeasurement = request.Type != "Profiles";

        var photo = await _dbContext.Photos
            .Where(p => p.UserId == request.UserId && p.Id == request.PhotoId)
            .FirstOrDefaultAsync(cancellationToken);

        if(photo == null)
        {
            throw new NotFoundEntityException(request.PhotoId, nameof(Domain.Entities.Photo));
        }

        _dbContext.Photos.Remove(photo);

        await _dbContext.SaveChangesAsync(cancellationToken);

        await _fileService.DeleteSelectedPhoto(request.Options.Value.ContainerName,request.UserId,photoType,photo.PhotoPath);

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == request.UserId && p.IsMeasurement == isMeasurement)
            .ToListAsync(cancellationToken);

        foreach (var elem in photos)
        {
            elem.PhotoPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, request.UserId, photoType, elem.PhotoPath);
        }

        return _mapper.Map<List<PhotoVm>>(photos);
    }
}
