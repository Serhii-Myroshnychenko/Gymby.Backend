using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Measurements.Commands.EditMeasurement;
using Gymby.Domain.Enums;

namespace Gymby.WebApi.Models;

public class EditMeasurementDto : IMapWith<EditMeasurementCommand>
{
    public string Id { get; set; } = null!;
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditMeasurementDto, EditMeasurementCommand>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.Date,
                vm => vm.MapFrom(v => v.Date))
            .ForMember(p => p.Type,
                vm => vm.MapFrom(v => v.Type))
            .ForMember(p => p.Value,
                vm => vm.MapFrom(v => v.Value))
            .ForMember(p => p.Unit,
                vm => vm.MapFrom(v => v.Unit));
    }
}

