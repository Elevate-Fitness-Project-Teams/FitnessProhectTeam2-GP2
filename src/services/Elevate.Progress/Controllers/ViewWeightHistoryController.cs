using Elevate.Progress.Features.ViewWeightHistory.DTOS;
using Elevate.Progress.Features.ViewWeightHistory.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.Progress.Controllers
{
    [ApiController]
    [Route("api/v1/progress")]
    [Authorize]
    public class ViewWeightHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewWeightHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("weight-history/{userId:guid}")]
        [ProducesResponseType(typeof(WeightHistoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetWeightHistory(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetWeightHistoryQuery(
                    new WeightHistoryRequestDto
                    {
                        UserId = userId
                    }),
                cancellationToken);

            return Ok(result);
        }
    }
}