using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;
using System.Text.Json.Serialization;

namespace Gymby.WebApi.Models;

public class UpdateProfileDto : IMapWith<UpdateProfileCommand>
{
    public string ProfileId { get; set; } = null!;
    public string? Username { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Description { get; set; }
    public IFormFile? Avatar { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TelegramUsername { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProfileDto, UpdateProfileCommand>()
            .ForMember(p => p.ProfileId,
                pt => pt.MapFrom(dt => dt.ProfileId))
            .ForMember(p => p.Username,
                pt => pt.MapFrom(dt => dt.Username))
            .ForMember(p => p.Email,
                pt => pt.MapFrom(dt => dt.Email))
            .ForMember(p => p.FirstName,
                pt => pt.MapFrom(dt => dt.FirstName))
            .ForMember(p => p.LastName,
                pt => pt.MapFrom(dt => dt.LastName))
            .ForMember(p => p.Description,
                pt => pt.MapFrom(dt => dt.Description))
            .ForMember(p => p.Avatar,
                pt => pt.MapFrom(dt => dt.Avatar))
            .ForMember(p => p.InstagramUrl,
                pt => pt.MapFrom(dt => dt.InstagramUrl))
            .ForMember(p => p.FacebookUrl,
                pt => pt.MapFrom(dt => dt.FacebookUrl))
            .ForMember(p => p.TelegramUsername,
                pt => pt.MapFrom(dt => dt.TelegramUsername));
    }
}
