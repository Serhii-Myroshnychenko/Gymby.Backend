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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMyMeasurements()
    {
        var query = new GetMyMeasurementsQuery(_config)
        {
            UserId = UserId.ToString()
        };

        return Ok(await Mediator.Send(query));
    }
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> AddMeasurement([FromBody]MeasurementDto measurement)
    {
        var command = _mapper.Map<AddMeasurementCommand>(measurement);

        command.UserId = UserId.ToString();

        return Ok(await Mediator.Send(command));
    }
    [Authorize]
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteMeasurement([FromBody] DeleteMeasurementDto measurement)
    {
        var command = _mapper.Map<DeleteMeasurementCommand>(measurement);

        command.UserId = UserId.ToString();

        return Ok(await Mediator.Send(command));
    }
}
