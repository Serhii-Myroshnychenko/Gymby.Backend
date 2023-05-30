using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;

namespace Gymby.WebApi.Models;

public class AddPhotoDto : IMapWith<AddPhotoCommand>
{
    public IFormFile Photo { get; set; } = null!;
    public DateTime? MeasurementDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddPhotoDto, AddPhotoCommand>()
            .ForMember(p => p.MeasurementDate,
                vm => vm.MapFrom(v =>v.MeasurementDate))
            .ForMember(p => p.Photo,
                vm => vm.MapFrom(v => v.Photo));
    }
}
