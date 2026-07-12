using Elevate.Progress.Domain.Enums;
using Elevate.Progress.Features.ViewProgressDashboard.DTOS;
using Elevate.Progress.Features.ViewProgressDashboard.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class ViewProgressDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewProgressDashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewProgressDashboardResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDashboard(
            [FromQuery] ProgressPeriod? period,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                              ?? User.FindFirstValue("sub");

            if (string.IsNullOrWhiteSpace(userIdClaim) ||
                !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(
                new ViewProgressDashboardOrchestrator(
                    new ViewProgressDashboardRequestDto
                    {
                        UserId = userId,
                        Period = period,
                        StartDate = startDate,
                        EndDate = endDate
                    }),
                cancellationToken);

            return Ok(result);
        }
    }
}
