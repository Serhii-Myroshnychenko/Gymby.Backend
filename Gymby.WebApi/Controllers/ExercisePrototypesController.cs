using Gymby.Application.Mediatr.ExercisePrototypes.Queries.GetAllExercisePrototypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers;

[Route("api/diary")]
[ApiController]
public class ExercisePrototypesController : BaseController
{       
    [Authorize]
    [HttpGet("exercise-prototypes")]
    public async Task<IActionResult> GetExercisePrototypes()
    {
        var query = new GetAllExercisePrototypesQuery()
        {
            UserId = UserId.ToString()
        };

        return Ok(await Mediator.Send(query));
    }
}
