using Elevate.Profile.Application.Features.Profile.Queries;
using Elevate.Profile.Application.Features.Profiles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

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

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(string firstName,string LastName,string Email,string PhoneNumber)
        {
           await _mediator.Send(new UpdateProfileCommand(firstName, LastName, Email, PhoneNumber));
           return Ok();
        }
    }
}
