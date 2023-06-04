using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Programs.Commands.UpdateProgram;

namespace Gymby.WebApi.Models;

public class UpdateProgramDto : IMapWith<UpdateProgramCommand>
{
    public string Name { get; set; } = null!;
    public string ProgramId { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string Type { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProgramDto, UpdateProgramCommand>()
            .ForMember(p => p.Name,
                pt => pt.MapFrom(dt => dt.Name))
            .ForMember(p => p.ProgramId,
                pt => pt.MapFrom(dt => dt.ProgramId))
            .ForMember(p => p.Description,
                pt => pt.MapFrom(dt => dt.Description))
            .ForMember(p => p.Level,
                pt => pt.MapFrom(dt => dt.Level))
            .ForMember(p => p.Type,
                pt => pt.MapFrom(dt => dt.Type));
    }
}
