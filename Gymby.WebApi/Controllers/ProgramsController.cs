using AutoMapper;
using Gymby.Application.Mediatr.Programs.Commands.CreateProgram;
using Gymby.WebApi.Models.CreateProgramDtos;
using Microsoft.AspNetCore.Mvc;

namespace Gymby.WebApi.Controllers
{
    [Route("api/program")]
    [ApiController]
    public class ProgramsController : BaseController
    {
        private readonly IMapper _mapper;

        public ProgramsController(IMapper mapper) =>
            (_mapper) = (mapper);

        [HttpPost("create")]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramDto request)
        {
            var command = _mapper.Map<CreateProgramCommand>(request);
            command.UserId = UserId.ToString();

            return Ok(await Mediator.Send(command));
        }
    }
}
