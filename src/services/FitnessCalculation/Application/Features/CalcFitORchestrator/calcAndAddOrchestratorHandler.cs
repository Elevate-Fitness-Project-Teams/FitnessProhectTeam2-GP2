using Elevate.FitnessCalculation.Application.Features.AddCalulatedMetric;
using Elevate.FitnessCalculation.Application.Features.CalculateFitness;
using Elevate.Profile.Domain.Interfaces;
using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.CalcFitORchestrator
{
    public class calcAndAddOrchestratorHandler : IRequestHandler<CalcAndAddOrchestrator>
    {
        private readonly IMediator _mediator;

        public calcAndAddOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Unit> Handle(CalcAndAddOrchestrator request, CancellationToken cancellationToken)
        {
            var meticCalculation =await _mediator.Send(new CalculateUserFitnessCommand(request.userId), cancellationToken);
            await _mediator.Send(new AddCalculatedMetricCommand(meticCalculation.UserId, meticCalculation.bmr, meticCalculation.tdee, meticCalculation.calorieTarget), cancellationToken);
            return Unit.Value;
        }
    }
}
