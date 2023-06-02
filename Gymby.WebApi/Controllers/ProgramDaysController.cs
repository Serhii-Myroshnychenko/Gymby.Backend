using Gymby.Application.Mediatr.ProgramDays.Commands.CreateProgramDay;
using Gymby.Application.Mediatr.ProgramDays.Commands.DeleteProgramDay;
using Gymby.Application.Mediatr.ProgramDays.Commands.UpdateProgramDay;
using Gymby.WebApi.Models.ProgramDayDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProgramDaysController : BaseController
    {
        public ProgramDaysController()
        {
        }

        [Authorize]
        [HttpPost("program/day/create")]
        public async Task<IActionResult> CreateProgramDay([FromBody] CreateProgramDayDto request)
        {
            var command = new CreateProgramDayCommand()
            {
                UserId = UserId.ToString(),
                Name = request.Name,
                ProgramId = request.ProgramId
            };

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("program/day/update")]
        public async Task<IActionResult> UpdateProgramDay([FromBody] UpdateProgramDayDto request)
        {
            var command = new UpdateProgramDayCommand()
            {
                UserId = UserId.ToString(),
                Name = request.Name,
                ProgramId = request.ProgramId,
                ProgramDayId = request.ProgramDayId
            };

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("program/day/delete")]
        public async Task<IActionResult> DeleteProgramDay([FromBody] DeleteProgramDayDto request)
        {
            var command = new DeleteProgramDayCommand()
            {
                UserId = UserId.ToString(),
                ProgramId = request.ProgramId,
                ProgramDayId = request.ProgramDayId
            };

            return Ok(await Mediator.Send(command));
        }
    }
}
