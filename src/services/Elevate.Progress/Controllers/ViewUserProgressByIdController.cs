using Elevate.Progress.Features.ViewUserProgressById.DTOS;
using Elevate.Progress.Features.ViewUserProgressById.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class ViewUserProgressByIdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewUserProgressByIdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(ViewUserProgressByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProgressById(
            Guid userId,
            CancellationToken cancellationToken)
        {
            // TODO: Replace "UserId" with the actual claim name from the Authentication Service.
            var userIdClaim = User.FindFirst("UserId")?.Value;

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
                new ViewUserProgressByIdOrchestrator(
                    new ViewUserProgressByIdRequestDto
                    {
                        UserId = userId
                    }),
                cancellationToken);

            return Ok(result);
        }
    }
}