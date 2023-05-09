using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasurementPhoto;

namespace Gymby.WebApi.Models;

public class MeasurementPhotoDto : IMapWith<AddMeasurementPhotoCommand>
{
    public IFormFile File { get; set; } = null!;
    public DateTime MeasurementDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MeasurementPhotoDto, AddMeasurementPhotoCommand>()
            .ForMember(p => p.File,
                vm => vm.MapFrom(v => v.File))
            .ForMember(p => p.MeasurementDate,
                vm => vm.MapFrom(v => v.MeasurementDate));
    }
}
