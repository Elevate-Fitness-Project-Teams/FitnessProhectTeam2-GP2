using System.Security.Claims;
using Elevate.Progress.Features.ViewProgressStats.DTOS;
using Elevate.Progress.Features.ViewProgressStats.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class ViewProgressStatsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewProgressStatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("stats/{userId:guid}")]
        [ProducesResponseType(typeof(ViewProgressStatsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProgressStats(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                              ?? User.FindFirstValue("sub");

            if (string.IsNullOrWhiteSpace(userIdClaim) ||
                !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return Unauthorized();
            }

            if (currentUserId != userId)
            {
                return Forbid();
            }

            var result = await _mediator.Send(
                new ViewProgressStatsOrchestrator(
                    new ViewProgressStatsRequestDto
                    {
                        UserId = userId
                    }),
                cancellationToken);

            return Ok(result);
        }
    }
}