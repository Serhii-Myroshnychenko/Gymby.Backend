using AutoMapper;
using Gymby.Application.Mediatr.ProgramAccesses.AccessProgramToUserByUsername;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Commands.DeleteProgram;
using Gymby.Application.Mediatr.Programs.Queries.GetAllProgramsInDiary;
using Gymby.Application.Mediatr.Programs.Queries.GetFreePrograms;
using Gymby.Application.Mediatr.Programs.Queries.GetPersonalPrograms;
using Gymby.Application.Mediatr.Programs.Queries.GetProgramById;
using Gymby.Application.Mediatr.Programs.Queries.GetProgramsFromCoach;
using Gymby.WebApi.Models;
using Gymby.WebApi.Models.CreateProgramDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProgramsController : BaseController
    {
        private readonly IMapper _mapper;

        public ProgramsController(IMapper mapper) =>
            (_mapper) = (mapper);

        [Authorize]
        [HttpPost("program/create")]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto request)
        {
            var command = _mapper.Map<CreateProgramCommand>(request);
            command.UserId = UserId.ToString();

            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("program/{id}")]
        public async Task<IActionResult> GetProgramById(string id)
        {
            var query = new GetProgramByIdQuery()
            {
                ProgramId = id,
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpGet("programs/free")]
        public async Task<IActionResult> GetFreePrograms()
        {
            var query = new GetFreeProgramsQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpGet("programs/shared")]
        public async Task<IActionResult> GetSharedPrograms()
        {
            var query = new GetProgramsFromCoachQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpGet("programs/personal")]
        public async Task<IActionResult> GetPersonalPrograms()
        {
            var query = new GetPersonalProgramsQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpPost("program/access")]
        public async Task<IActionResult> AccessProgramToUserByUsername([FromBody] AccessProgramToUserByUsernameDto request)
        {
            var command = new AccessProgramToUserByUsernameQuery()
            {
                UserId = UserId.ToString(),
                ProgramId = request.ProgramId,
                Username = request.Username
            };

            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpGet("diary/all-programs")]
        public async Task<IActionResult> GetAllProgramsToDiary()
        {
            var query = new GetAllProgramsInDiaryQuery()
            {
                UserId = UserId.ToString()
            };

            return Ok(await Mediator.Send(query));
        }

        [Authorize]
        [HttpPost("program/delete")]
        public async Task<IActionResult> RemoveProgram([FromBody] DeleteProgramDto request)
        {
            var command = new DeleteProgramCommand()
            {
                UserId = UserId.ToString(),
                ProgramId = request.ProgramId
            };

            return Ok(await Mediator.Send(command));
        }
    }
}
