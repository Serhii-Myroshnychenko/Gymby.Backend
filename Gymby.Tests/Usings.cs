global using Xunit;
global using Gymby.Persistence.Data;
global using Gymby.Application.Mediatr.Profiles.Commands.UpdateProfile;
global using Microsoft.EntityFrameworkCore;
global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Gymby.Domain.Entities;
global using Gymby.Application.Interfaces;
global using Gymby.UnitTests.Common;
global using Gymby.UnitTests.Common.Measurements;
global using Gymby.UnitTests.Common.Photos;
global using Gymby.UnitTests.Common.Profiles;
global using Microsoft.Extensions.Options;
global using Gymby.Application.ViewModels;
global using Gymby.Application.Config;
global using Shouldly;
global using Gymby.Application.Common.Exceptions;
global using Gymby.Application.Utils;
global using Moq;
global using Microsoft.AspNetCore.Http;
global using Gymby.Domain.Enums;
global using Gymby.UnitTests.Services;
global using MediatR;
global using FluentAssertions;