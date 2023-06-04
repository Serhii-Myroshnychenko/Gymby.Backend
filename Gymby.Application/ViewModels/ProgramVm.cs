using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;

namespace Gymby.Application.ViewModels;

public class ProgramVm : IMapWith<Program>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string Description { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<ProgramDayVm>? ProgramDays { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Program, ProgramVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.IsPublic,
                vm => vm.MapFrom(v => v.IsPublic))
            .ForMember(p => p.Description,
                vm => vm.MapFrom(v => v.Description))
            .ForMember(p => p.Level,
                vm => vm.MapFrom(v => v.Level.ToString()))
            .ForMember(p => p.Type,
                vm => vm.MapFrom(v => v.Type.ToString()))
            .ForMember(p => p.ProgramDays,
                vm => vm.MapFrom(v => v.ProgramDays));
    }
}
