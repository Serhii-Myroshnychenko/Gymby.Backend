using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;

namespace Gymby.WebApi.Models;

public class DeleteMeasurementDto : IMapWith<DeleteMeasurementCommand>
{
    public string Id { get; set; } = null!;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteMeasurementDto, DeleteMeasurementCommand>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id));
    }
}
