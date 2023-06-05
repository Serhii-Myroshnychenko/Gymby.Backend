using Gymby.Application.Mediatr.Diaries.Command.ImportProgram;
using Gymby.Application.Mediatr.Diaries.Command.ImportProgramDay;
using Gymby.Application.Mediatr.DiaryAccesses.Commands.AccessToMyDiaryByUsername;
using Gymby.Application.Mediatr.DiaryAccesses.Queries.GetAllAvailableDiaries;
using Gymby.Application.Mediatr.DiaryDay.Queries.GetDiaryDay;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers
{
    [Route("api/diary")]
    [ApiController]
    public class DiariesController : BaseController
    {
        [Authorize]
        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailableDiaries()
        {
            var query = new GetAllAvailableDiariesQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }

        [Authorize]
        [HttpPost("access")]
        public async Task<IActionResult> AccessToMyDiaryByUsername([FromBody] AccessToMyDiaryByUsername request)
        {
            var command = new AccessToMyDiaryByUsernameCommand()
            {
                UserId = UserId.ToString(),
                Username = request.Username
            };

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("import/program-day")]
        public async Task<IActionResult> ImportProgramDayToDiary([FromBody] ImportProgramDayToDiaryDto request)
        {
            var command = new ImportProgramDayCommand()
            {
                Date = request.Date,
                DiaryId = request.DiaryId,
                ProgramDayId = request.ProgramDayId,
                ProgramId = request.ProgramId,
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPost("day")]
        public async Task<IActionResult> GetDiaryDay([FromBody] GetDiaryDayDto request)
        {
            var query = new GetDiaryDayCommand()
            {
                Date = request.Date,
                DiaryId = request.DiaryId,
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }

        [Authorize]
        [HttpPost("import/program")]
        public async Task<IActionResult> ImportProgram([FromBody] ImportProgramDto request)
        {
            var command = new ImportProgramCommand()
            {
                StartDate = request.StartDate,
                DiaryId = request.DiaryId,
                UserId = UserId.ToString(),
                DaysOfWeek = request.DaysOfWeek,
                ProgramId = request.ProgramId
            };

            return Ok(await Mediator.Send(command));
        }
    }
}
