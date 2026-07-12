using Elevate.Progress.Features.LogWorkoutCompletion.DTOS;
using Elevate.Progress.Features.LogWorkoutCompletion.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress/workouts")]
    [Authorize]
    public class LogWorkoutCompletionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogWorkoutCompletionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LogWorkoutCompletionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LogWorkoutCompletion(
            [FromBody] LogWorkoutCompletionRequestDto request,
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

            var response = await _mediator.Send(
                new LogWorkoutCompletionOrchestrator(request),
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}