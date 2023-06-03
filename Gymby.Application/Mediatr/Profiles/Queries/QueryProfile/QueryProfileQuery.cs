using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Profiles.Queries.QueryProfile;

public class QueryProfileQuery : IRequest<List<ProfileVm>>
{
    public string? Type { get; set; }
    public string? Query { get; set; }
}
