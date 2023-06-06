using Gymby.Application.Mediatr.Statistics.Queries.GetAllNumberStatistics;
using Gymby.Application.Mediatr.Statistics.Queries.GetApproachesDoneCouneByDate;
using Gymby.Application.Mediatr.Statistics.Queries.GetExercisesDoneCountByDate;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : BaseController
    {
        [Authorize]
        [HttpGet("numeric")]
        public async Task<IActionResult> GetAllNumericStatistics()
        {
            var query = new GetAllNumberStatisticsQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpPost("graph/exercises-done")]
        public async Task<IActionResult> GetExercisesDoneCountByDate([FromBody] ExercisesDoneCountByDateDto request)
        {
            var query = new GetExercisesDoneCountByDateQuery()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }

        [Authorize]
        [HttpPost("graph/approaches-done")]
        public async Task<IActionResult> GetApproachesDoneCountByDate([FromBody] ApproachesDoneByDateDto request)
        {
            var query = new GetApproachesDoneCountByDateQuery()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
    }
}
