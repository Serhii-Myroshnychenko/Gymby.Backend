using AutoMapper;
using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Common.Mappings;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramProgramDayDto : IMapWith<ProgramDayCM>
{
    public string Name { get; set; } = null!;
    public List<CreateProgramExerciseDto>? Exercises { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProgramProgramDayDto, ProgramDayCM>()
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.Exercises,
                vm => vm.MapFrom(v => v.Exercises));
    }
}
