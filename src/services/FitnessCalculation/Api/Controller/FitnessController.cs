using Elevate.FitnessCalculation.Application.Features.CalculateFitness;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Elevate.FitnessCalculation.Api.Controller
{
    [ApiController]
    [Route("api/v1/fitness")]
    public class FitnessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FitnessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateFitness(
            [FromBody] CalculateUserFitnessCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

    }
}
