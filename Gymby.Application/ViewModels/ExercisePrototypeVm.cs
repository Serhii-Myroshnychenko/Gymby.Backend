using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using Gymby.Domain.Enums;

namespace Gymby.Application.ViewModels;

public class ExercisePrototypeVm : IMapWith<ExercisePrototype>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Category Category { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<ExercisePrototype, ExercisePrototypeVm>()
            .ForMember(p => p.Id,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.Name,
                vm => vm.MapFrom(v => v.Name))
            .ForMember(p => p.Description,
                vm => vm.MapFrom(v => v.Description))
            .ForMember(p => p.Category,
                vm => vm.MapFrom(v => v.Category));
    }
}
