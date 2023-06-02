using AutoMapper;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.Application.Mediatr.Programs.Queries.GetProgramById;
using Gymby.WebApi.Models.CreateProgramDtos;
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

        [HttpPost("program/create")]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto request)
        {
            var command = _mapper.Map<CreateProgramCommand>(request);
            command.UserId = UserId.ToString();

            return Ok(await Mediator.Send(command));
        }

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
    }
}
