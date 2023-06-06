using AutoMapper;
using Gymby.Application.CommandModels.CreateProgramModels;
using Gymby.Application.Common.Mappings;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramApproacheDto : IMapWith<ApproachCM>
{
    public int Repeats { get; set; }
    public int Interval { get; set; }
    public double Weight { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProgramApproacheDto, ApproachCM>()
            .ForMember(p => p.Repeats,
                vm => vm.MapFrom(v => v.Repeats))
            .ForMember(p => p.Interval,
                vm => vm.MapFrom(v => v.Interval))
            .ForMember(p => p.Weight,
                vm => vm.MapFrom(v => v.Weight));
    }
}
