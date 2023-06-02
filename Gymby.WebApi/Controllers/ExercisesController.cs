using Gymby.Application.Mediatr.Exercises.Commands.CreateProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.DeleteProgramExercise;
using Gymby.Application.Mediatr.Exercises.Commands.UpdateProgramExercise;
using Gymby.WebApi.Models.ProgramExerciseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers;

[Route("api/")]
[ApiController]
public class ExercisesController : BaseController
{
    public ExercisesController()
    {
    }

    [Authorize]
    [HttpPost("program/exercise/update")]
    public async Task<IActionResult> UpdateProgramExercise([FromBody]UpdateProgramExerciseDto request)
    {
        var command = new UpdateProgramExerciseCommand()
        {
            UserId = UserId.ToString(),
            ExerciseId = request.ExerciseId,
            ExercisePrototypeId = request.ExercisePrototypeId,
            Name = request.Name,
            ProgramId = request.ProgramId,
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("program/exercise/create")]
    public async Task<IActionResult> CreateProgramExercise([FromBody]CreateProgramsExerciseDto request)
    {
        var command = new CreateProgramExerciseCommand()
        {
            UserId = UserId.ToString(),
            ExercisePrototypeId = request.ExercisePrototypeId,
            ProgramDayId = request.ProgramDayId,
            Name = request.Name,
            ProgramId = request.ProgramId
        };

        return Ok(await Mediator.Send(command));
    }

    [Authorize]
    [HttpPost("program/exercise/delete")]
    public async Task<IActionResult> DeleteProgramExercise([FromBody] DeleteProgramExerciseDto request)
    {
        var command = new DeleteProgramExerciseCommand()
        {
            UserId = UserId.ToString(),
            ExerciseId = request.ExerciseId,
            ProgramId = request.ProgramId
        };

        return Ok(await Mediator.Send(command));
    }
}
