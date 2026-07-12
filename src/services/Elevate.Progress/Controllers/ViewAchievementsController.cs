using System.Security.Claims;
using Elevate.Progress.Features.ViewAchievements.DTOS;
using Elevate.Progress.Features.ViewAchievements.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class ViewAchievementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewAchievementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("achievements")]
        [ProducesResponseType(typeof(ViewAchievementsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAchievements(
            CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                              ?? User.FindFirstValue("sub");

            if (string.IsNullOrWhiteSpace(userIdClaim) ||
                !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(
                new ViewAchievementsOrchestrator(
                    new ViewAchievementsRequestDto
                    {
                        UserId = currentUserId
                    }),
                cancellationToken);

            return Ok(result);
        }
    }
}