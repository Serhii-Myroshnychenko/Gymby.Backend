using AutoMapper;
using Gymby.Application.CommandModels;
using Gymby.Application.Common.Mappings;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramExerciseDto : IMapWith<ExerciseCM>
{
    public string Name { get; set; } = null!;
    public string ExercisePrototypeId { get; set; } = null!;
    public List<CreateProgramApproacheDto>? Approaches { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProgramExerciseDto, ExerciseCM>()
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.ExercisePrototypeId,
                vm => vm.MapFrom(v => v.ExercisePrototypeId))
            .ForMember(p => p.Approaches,
                vm => vm.MapFrom(v => v.Approaches));
    }
}
