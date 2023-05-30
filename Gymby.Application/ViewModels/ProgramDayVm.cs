using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;

namespace Gymby.Application.ViewModels;

public class ProgramDayVm : IMapWith<ProgramDay>
{
    public string Id { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<ExerciseVm>? Exercises { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<ProgramDay, ProgramDayVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.ProgramId,
                vm => vm.MapFrom(v => v.ProgramId))
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.Exercises,
                vm => vm.MapFrom(v => v.Exercises));
    }
}
