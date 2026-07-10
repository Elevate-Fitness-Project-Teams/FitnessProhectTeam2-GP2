using Elevate.Profile.Application.Features.Profile.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Profile.Api.Controllers
{

    [ApiController]
    [Route("api/v1/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProfileQuery(), cancellationToken);

            return Ok(result);
        }
    }
}
