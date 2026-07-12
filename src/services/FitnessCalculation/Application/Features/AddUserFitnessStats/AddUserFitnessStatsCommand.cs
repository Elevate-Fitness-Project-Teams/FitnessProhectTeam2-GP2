using MediatR;

namespace Elevate.FitnessCalculation.Application.Features.AddUserFitnessStats
{
    //userId, weight, height, age, gender, goal, activityLevel
    public record AddUserFitnessStatsCommand(
        int UserId,
        double Weight,
        double Height,
        int Age,
        string Gender,
        string Goal,
        string ActivityLevel
    ) : IRequest;
}
