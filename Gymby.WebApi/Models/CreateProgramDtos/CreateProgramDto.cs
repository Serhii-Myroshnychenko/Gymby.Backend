using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Domain.Enums;

namespace Gymby.WebApi.Models.CreateProgramDtos;

public class CreateProgramDto : IMapWith<CreateProgramCommand>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Level { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<CreateProgramProgramDayDto>? ProgramDays { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProgramDto, CreateProgramCommand>()
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.Description,
                vm => vm.MapFrom(v => v.Description))
            .ForMember(p => p.Level,
                vm => vm.MapFrom(v => v.Level))
            .ForMember(p => p.Type,
                vm => vm.MapFrom(v => v.Type))
            .ForMember(p => p.ProgramDays,
                vm => vm.MapFrom(v => v.ProgramDays));
    }
}
