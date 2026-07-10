using Elevate.Progress.Features.LogWeightEntry.DTOS;
using Elevate.Progress.Features.LogWeightEntry.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class LogWeightEntryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogWeightEntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("weight")]
        [ProducesResponseType(typeof(LogWeightEntryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> LogWeightEntry(
            [FromBody] LogWeightEntryRequestDto request,
            CancellationToken cancellationToken)
        {
            // TODO: Replace "UserId" with the actual claim name from the Authentication Service.
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) ||
                !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return Unauthorized();
            }

            if (currentUserId != request.UserId)
            {
                return Forbid();
            }

            var result = await _mediator.Send(
                new LogWeightEntryOrchestrator(request),
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}