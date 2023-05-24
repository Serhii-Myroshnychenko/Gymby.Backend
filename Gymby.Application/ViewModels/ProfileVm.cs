using Gymby.Application.Common.Mappings;
using Gymby.Domain.Entities;
using System.Text.Json.Serialization;

namespace Gymby.Application.ViewModels;

public class ProfileVm : IMapWith<Profile>
{
    public string ProfileId { get; set; } = null!;
    public string Username { get; set; } = null!;
    [JsonIgnore]
    public string UserId { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public string? PhotoAvatarPath { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TelegramUsername { get; set; }
    public bool IsCoach { get; set; }
    public string Email { get; set; } = null!;
    public List<PhotoVm>? Photos { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Profile, ProfileVm>()
            .ForMember(p => p.ProfileId,
                vm => vm.MapFrom(v => v.Id))
            .ForMember(p => p.Username,
                vm => vm.MapFrom(v => v.Username))
            .ForMember(p => p.FirstName,
                vm => vm.MapFrom(v => v.FirstName))
            .ForMember(p => p.LastName,
                vm => vm.MapFrom(v => v.LastName))
            .ForMember(p => p.Description,
                vm => vm.MapFrom(v => v.Description))
            .ForMember(p => p.Username,
                vm => vm.MapFrom(v => v.Username))
            .ForMember(p => p.InstagramUrl,
                vm => vm.MapFrom(v => v.InstagramUrl))
            .ForMember(p => p.FacebookUrl,
                vm => vm.MapFrom(v => v.FacebookUrl))
            .ForMember(p => p.TelegramUsername,
                vm => vm.MapFrom(v => v.TelegramUsername))
            .ForMember(p => p.IsCoach,
                vm => vm.MapFrom(v => v.IsCoach))
            .ForMember(p => p.Email,
                vm => vm.MapFrom(v => v.Email))
            .ForMember(p => p.UserId,
                vm => vm.MapFrom(v => v.UserId))
            .ForMember(p => p.PhotoAvatarPath,
                vm => vm.MapFrom(v => v.PhotoAvatarPath));
    }
}
