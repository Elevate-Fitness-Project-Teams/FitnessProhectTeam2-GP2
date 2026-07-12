using Elevate.FitnessCalculation.Application.Features.AddUserFitnessStats;
using Elevate.FitnessCalculation.Application.Features.CalculateFitness;
using Elevate.FitnessCalculation.Application.Features.DTOS;
using Elevate.FitnessCalculation.Application.Features.GetAllConfigPlans;
using Elevate.FitnessCalculation.Application.Features.GetFitnessStats;
using Elevate.FitnessCalculation.Application.Features.GetMetricCalc;
using Elevate.FitnessCalculation.Application.Features.GetPlanConfig;
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

        [HttpPost("weight-goal-activity")]
        public async Task<IActionResult> CalculateWeightGoalActivity(
            [FromBody] AddUserFitnessStatsCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }


        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateFitness(
            [FromBody] CalculateUserFitnessCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet("metrics/{userId}")]
        public async Task<IActionResult> GetFitnessMetrics(int userId)
        {
            var result = await _mediator.Send(new GetFitnessMetricsQuery(userId));

            return Ok(result);
        }
        [HttpGet("stats/{userId:int}")]

        public async Task<IActionResult> GetFitnessStats(int userId)
        {
            var result = await _mediator.Send(new GetFitnessStatQuery(userId));

            return Ok(result);
        }

        [HttpGet("plans/{planId:string}")]
        public async Task<IActionResult> GetPlan(string planId)
        {
            var result = await _mediator.Send(new GetPlanConfigQuery(planId));

            return Ok(result);
        }

        [HttpGet("plan-configs")]
        public async Task<IActionResult> GetAllPlans()
        {
            var result = await _mediator.Send(new GetAllConfigPlansQuery());
            return Ok(result);
        }

    }
}
