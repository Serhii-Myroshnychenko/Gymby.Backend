using AutoMapper;
using Gymby.Application.Interfaces;
using Gymby.Application.ViewModels;
using Gymby.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gymby.Application.Mediatr.Photos.Commands.AddPhoto;

public class AddPhotoHandler 
    : IRequestHandler<AddPhotoCommand, List<PhotoVm>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AddPhotoHandler(IApplicationDbContext dbContext, IMapper mapper, IFileService fileService) =>
        (_dbContext, _mapper, _fileService) = (dbContext, mapper, fileService);

    public async Task<List<PhotoVm>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var photoType = request.Type == "Profiles" ? request.Options.Value.Profile : request.Options.Value.Measurement;
        var isMeasurement = request.Type != "Profiles";

        var newPhotoName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);

        await _fileService.AddPhotoAsync(request.Options.Value.ContainerName, request.UserId, photoType, request.Photo, newPhotoName);

        var photo = new Photo()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            PhotoPath = newPhotoName,
            IsMeasurement = isMeasurement,
            MeasurementDate = request.MeasurementDate,
            CreationDate = DateTime.Now
        };

        await _dbContext.Photos.AddAsync(photo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var photos = await _dbContext.Photos
            .Where(p => p.UserId == request.UserId && p.IsMeasurement == isMeasurement)
            .ToListAsync(cancellationToken);

        foreach(var elem in photos)
        {
            elem.PhotoPath = await _fileService.GetPhotoAsync(request.Options.Value.ContainerName, request.UserId, photoType, elem.PhotoPath);
        }

        return _mapper.Map<List<PhotoVm>>(photos);
    }
}
