using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;

namespace Gymby.Application.ViewModels;

public class ApproachVm : IMapWith<Approach>
{
    public string Id { get; set; } = null!;
    public string ExerciseId { get; set; } = null!;
    public int Repeats { get; set; }
    public double Weight { get; set; }
    public bool IsDone { get; set; }
    public int Interval { get; set; }
    public DateTime CreationDate { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Approach, ApproachVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.ExerciseId,
                vm => vm.MapFrom(v => v.ExerciseId))
            .ForMember(p => p.Repeats,
                vm => vm.MapFrom(v => v.Repeats))
            .ForMember(p => p.Weight,
                vm => vm.MapFrom(v => v.Weight))
            .ForMember(p => p.IsDone,
                vm => vm.MapFrom(v => v.IsDone))
            .ForMember(p => p.Interval,
                vm => vm.MapFrom(v => v.Interval))
            .ForMember(p => p.CreationDate,
                vm => vm.MapFrom(v => v.CreationDate));
    }
}
