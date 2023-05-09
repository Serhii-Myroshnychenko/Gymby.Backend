using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using System.Text.Json.Serialization;

namespace Gymby.Application.ViewModels;

public class PhotoVm : IMapWith<Photo>
{
    public string Id { get; set; } = null!;
    [JsonIgnore]
    public string UserId { get; set; } = null!;
    public string PhotoPath { get; set; } = null!;
    public bool IsMeasurement { get; set; }
    public DateTime? MeasurementDate { get; set; }
    public DateTime CreationDate { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Photo, PhotoVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.PhotoPath,
                vm => vm.MapFrom(v => v.PhotoPath))
            .ForMember(p => p.IsMeasurement,
                vm => vm.MapFrom(v => v.IsMeasurement))
            .ForMember(p => p.MeasurementDate,
                vm => vm.MapFrom(v => v.MeasurementDate))
            .ForMember(p => p.CreationDate,
                vm => vm.MapFrom(v => v.CreationDate))
            .ForMember(p => p.UserId,
                vm => vm.MapFrom(v => v.UserId));
    }
}
