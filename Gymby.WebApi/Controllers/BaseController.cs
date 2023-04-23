using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gymby.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    internal Guid UserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
}
