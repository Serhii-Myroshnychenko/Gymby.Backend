using Gymby.Application.Mediatr.Approaches.Commands.CreateProgramApproach;
using Gymby.Application.Mediatr.Approaches.Commands.DeleteProgramApproach;
using Gymby.Application.Mediatr.Approaches.Commands.UpdateProgramApproach;
using Gymby.WebApi.Models.ProgramApproachDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers;

[Route("api/")]
[ApiController]
public class ApproachesController : BaseController
{
    public ApproachesController()
    {
    }

    [Authorize]
    [HttpPost("program/approach/create")]
    public async Task<IActionResult> CreateProgramApproach([FromBody] CreateProgramApproachDto request)
    {
        var command = new CreateProgramApproachCommand()
        {
            ExerciseId = request.ExerciseId,
            ProgramId = request.ProgramId,
            Repeats = request.Repeats,
            UserId = UserId.ToString(),
            Weight = request.Weight
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("program/approach/update")]
    public async Task<IActionResult> UpdateProgramApproach([FromBody] UpdateProgramApproachDto request)
    {
        var command = new UpdateProgramApproachCommand()
        {
            ExerciseId = request.ExerciseId,
            ProgramId = request.ProgramId,
            Repeats = request.Repeats,
            UserId = UserId.ToString(),
            Weight = request.Weight,
            ApproachId = request.ApproachId,
            IsDone = request.IsDone
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("program/approach/delete")]
    public async Task<IActionResult> DeleteProgramApproach([FromBody] DeleteProgramApproachDto request)
    {
        var command = new DeleteProgramApproachCommand()
        {
            ExerciseId = request.ExerciseId,
            ProgramId = request.ProgramId,
            UserId = UserId.ToString(),
            ApproachId = request.ApproachId
        };

        return Ok(await Mediator.Send(command));
    }
}
