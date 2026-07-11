using Elevate.Workout.Features.Exercises.GetPagniatedExcercise;
using FluentValidation;
using SharedKernel.Pagniation;

public sealed class GetExercisesValidator : AbstractValidator<GetExercisesQuery>
{
    public GetExercisesValidator()
    {
        RuleFor(x => x.PageNumber).MustBeValidPageNumber();
        RuleFor(x => x.PageSize).MustBeValidPageSize(maxPageSize: 50);
    }
}