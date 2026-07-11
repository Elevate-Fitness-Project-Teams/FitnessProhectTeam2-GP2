using Elevate.Profile.Application.Features.Profile.Queries;
using Elevate.Profile.Application.Features.Profiles.Commands;
using Elevate.Profile.Application.Features.Profiles.UpdatePasswords;
using Elevate.Profile.Application.Features.Profiles.UpdateSettings;
using Elevate.Profile.Application.Features.Profiles.viewSettingss;
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
        [HttpPut("change-password")]
        public async Task<IActionResult> UpdatePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            await _mediator.Send(new ChangePasswordCommand(currentPassword, newPassword, confirmPassword));
            return Ok();
        }

        [HttpGet("view-settings")]
        public async Task<IActionResult> GetViewSettings(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ViewSettingsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings(
                    [FromBody] updateSettingsCommand command,
                    CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

    }
}
