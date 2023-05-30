using AutoMapper;
using Gymby.Application.CommandModels;
using Gymby.Application.Common.Mappings;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramApproacheDto : IMapWith<ApproachCM>
{
    public int Repeats { get; set; }
    public double Weight { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProgramApproacheDto, ApproachCM>()
            .ForMember(p => p.Repeats,
                vm => vm.MapFrom(v => v.Repeats))
            .ForMember(p => p.Weight,
                vm => vm.MapFrom(v => v.Weight));
    }
}
