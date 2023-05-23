using AutoMapper;
using Gymby.Application.Config;
using Gymby.Application.Mediatr.Photos.Commands.AddPhoto;
using Gymby.Application.Mediatr.Photos.Commands.DeletePhoto;
using Gymby.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gymby.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOptions<AppConfig> _config;

        public PhotosController(IMapper mapper, IOptions<AppConfig> config) =>
            (_mapper, _config) = (mapper, config);

        [HttpPost("profile")]
        public async Task<IActionResult> AddPhotoInProfile([FromForm] AddPhotoDto request)
        {
            var command = new AddPhotoCommand(_config, _config.Value.Profile) { Photo = request.Photo, MeasurementDate = request.MeasurementDate, UserId = "guid" };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("measurement")]
        public async Task<IActionResult> AddPhotoInMeasurement([FromForm] AddPhotoDto request)
        {
            var command = new AddPhotoCommand(_config, _config.Value.Measurement) { Photo = request.Photo, MeasurementDate = request.MeasurementDate, UserId = "guid" };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("profile/delete")]
        public async Task<IActionResult> DeletePhotoFromProfile([FromForm] DeletePhotoDto request)
        {
            var command = new DeletePhotoCommand(_config, _config.Value.Profile) { PhotoId = request.PhotoId, UserId = "guid" };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("measurement/delete")]
        public async Task<IActionResult> DeleteMeasurementFromProfile([FromForm] DeletePhotoDto request)
        {
            var command = new DeletePhotoCommand(_config, _config.Value.Measurement) { PhotoId = request.PhotoId, UserId = "guid" };
            return Ok(await Mediator.Send(command));
        }
    }
}
