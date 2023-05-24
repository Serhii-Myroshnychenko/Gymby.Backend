using AutoMapper;
using Gymby.Application.Config;
using Gymby.Application.Mediatr.Measurements.Commands.AddMeasuement;
using Gymby.Application.Mediatr.Measurements.Commands.DeleteMeasurement;
using Gymby.Application.Mediatr.Measurements.Queries.GetMyMeasurements;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gymby.WebApi.Controllers;

public class MeasurementsController : BaseController
{
    private readonly IOptions<AppConfig> _config;
    private readonly IMapper _mapper;

    public MeasurementsController(IOptions<AppConfig> config, IMapper mapper) =>
        (_config, _mapper) = (config, mapper);

    [HttpGet]
    public async Task<IActionResult> GetMyMeasurements()
    {
        var query = new GetMyMeasurementsQuery(_config)
        {
            //UserId = UserId.ToString()
            UserId = "guid",
        };

        return Ok(await Mediator.Send(query));
    }

    [HttpPost("create")]
    public async Task<IActionResult> AddMeasurement([FromForm]MeasurementDto measurement)
    {
        var command = _mapper.Map<AddMeasurementCommand>(measurement);

        //command.UserId = UserId.ToString();
        command.Options = _config;
        command.UserId = "guid";

        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteMeasurement([FromForm] DeleteMeasurementDto measurement)
    {
        var command = new DeleteMeasurementCommand(_config)
        {
            Id = measurement.Id,
            //UserId = UserId.ToString()
            UserId = "guid",
        };

        return Ok(await Mediator.Send(command));
    }
}
