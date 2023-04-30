using AutoMapper;
using Gymby.Application.Common.Mappings;
using Gymby.Application.Mediatr.Profiles.Commands.CreateProfile;

namespace Gymby.WebApi.Models;

public class CreateProfileDto : IMapWith<CreateProfileCommand>
{
    public string UserId { get; set; } = null!;
    public string Email { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProfileDto, CreateProfileCommand>()
            .ForMember(p => p.UserId,
                pt => pt.MapFrom(dt => dt.UserId))
            .ForMember(p => p.Email,
                pt => pt.MapFrom(dt => dt.Email));
    }
}
