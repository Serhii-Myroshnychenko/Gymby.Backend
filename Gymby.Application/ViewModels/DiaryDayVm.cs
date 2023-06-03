using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;

namespace Gymby.Application.ViewModels;

public class DiaryDayVm : IMapWith<DiaryDay>
{
    public string Id { get; set; } = null!;
    public string DiaryId { get; set; } = null!;
    public string? ProgramDayId { get; set; }
    public DateTime Date { get; set; }
    public ICollection<ExerciseVm>? Exercises { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<DiaryDay, DiaryDayVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.DiaryId,
                vm => vm.MapFrom(v => v.DiaryId))
            .ForMember(p => p.ProgramDayId,
                vm => vm.MapFrom(v => v.ProgramDayId))
            .ForMember(p => p.Date,
                vm => vm.MapFrom(v => v.Date))
            .ForMember(p => p.Exercises,
                vm => vm.MapFrom(v => v.Exercises));
    }
}
