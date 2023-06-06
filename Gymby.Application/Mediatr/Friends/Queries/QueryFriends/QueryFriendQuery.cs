﻿using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Friends.Queries.QueryFriends;

public class QueryFriendQuery : IRequest<List<ProfileVm>>
{
    public string? Type { get; set; }
    public string? Query { get; set; }
    public string UserId { get; set; } = null!;
    public IOptions<AppConfig> Options { get; set; } = null!;
}