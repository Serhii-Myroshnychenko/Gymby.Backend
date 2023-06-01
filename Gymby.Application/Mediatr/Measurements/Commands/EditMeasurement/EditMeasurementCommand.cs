﻿using Gymby.Application.Config;
using Gymby.Application.ViewModels;
using Gymby.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace Gymby.Application.Mediatr.Measurements.Commands.EditMeasurement;

public class EditMeasurementCommand : IRequest<MeasurementsList>
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime Date { get; set; }
    public MeasurementType Type { get; set; }
    public double Value { get; set; }
    public Units Unit { get; set; }
    public IOptions<AppConfig> Options { get; set; } = null!;
}
