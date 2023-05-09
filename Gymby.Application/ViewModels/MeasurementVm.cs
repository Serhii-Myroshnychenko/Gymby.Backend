using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;
using System.Text.Json.Serialization;

namespace Gymby.Application.ViewModels;

public class MeasurementVm : IMapWith<Measurement>
{
    public string Id { get; set; } = null!;
    [JsonIgnore]
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Measurement, MeasurementVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.UserId,
                vm => vm.MapFrom(v => v.UserId))
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
