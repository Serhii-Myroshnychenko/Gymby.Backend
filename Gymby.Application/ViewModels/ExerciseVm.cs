using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;

namespace Gymby.Application.ViewModels;

public class ExerciseVm : IMapWith<Exercise>
{
    public string Id { get; set; } = null!;
    public string? DiaryDayId { get; set; }
    public string ExercisePrototypeId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime? Date { get; set; }
    public string Name { get; set; } = null!;
    public List<ApproachVm>? Approaches { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Exercise, ExerciseVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.DiaryDayId,
                vm => vm.MapFrom(v => v.DiaryDayId))
            .ForMember(p => p.ExercisePrototypeId,
                vm => vm.MapFrom(v => v.ExercisePrototypeId))
            .ForMember(p => p.ProgramDayId,
                vm => vm.MapFrom(v => v.ProgramDayId))
            .ForMember(p => p.Date,
                vm => vm.MapFrom(v => v.Date))
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.Approaches,
                vm => vm.MapFrom(v => v.Approaches));
    }
}
