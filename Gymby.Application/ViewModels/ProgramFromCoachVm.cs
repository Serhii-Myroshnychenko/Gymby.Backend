﻿using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;

namespace Gymby.Application.ViewModels;

public class ProgramFromCoachVm : IMapWith<Program>
{
    public string CoachUsername { get; set; } = null!;
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool IsPublic { get; set; }
    public string Description { get; set; } = null!;
    public Level Level { get; set; }
    public ProgramType Type { get; set; }
    public List<ProgramDayVm>? ProgramDays { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Program, ProgramFromCoachVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.IsPublic,
                vm => vm.MapFrom(v => v.IsPublic))
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