﻿using Gymby.Application.ViewModels;
using MediatR;

namespace Gymby.Application.Mediatr.Programs.Queries.GetFreePrograms;

public class GetFreeProgramsQuery : IRequest<List<ProgramVm>>
{
    public string UserId { get; set; } = null!;
}