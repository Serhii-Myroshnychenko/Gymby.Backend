using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gymby.WebApi.Models;

public class MeasurementDto : IMapWith<AddMeasurementCommand>
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public MeasurementType Type { get; set; }
    [Required]
    public double Value { get; set; }
    [Required]
    public Units Unit { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MeasurementDto, AddMeasurementCommand>()
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
