using MediatR;
using Microsoft.AspNetCore.Mvc;
using Elevate.Nutrition.Api.Dtos.Responses;
using Elevate.Nutrition.Application.Features.Meals.Commands.CreateMeal;
using Elevate.Nutrition.Application.Features.Meals.Commands.DeleteMeal;
using Elevate.Nutrition.Application.Features.Meals.Commands.UpdateMeal;
using Elevate.Nutrition.Application.Features.Meals.Queries.GetAllMeals;
using Elevate.Nutrition.Application.Features.Meals.Queries.GetMealById;
using Elevate.Nutrition.Application.Features.Meals.Queries.SearchMealsByTags;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Api.Controllers;

[Route("api/nutrition/meals")]
[ApiController]
public class MealsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MealsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMealsQuery());
        return ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMealByIdQuery(id));
        return ToActionResult(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string tags)
    {
        var result = await _mediator.Send(new SearchMealsByTagsQuery(tags));
        return ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMealCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return ToActionResult(result);

        return CreatedAtAction(nameof(GetById), new { id = ((Result<int>)result).Value },
            ApiEnvelope.FromResult(result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMealCommand command)
    {
        if (id != command.Id)
            return BadRequest(ApiEnvelope.Failure(new List<string> { "Route id and body id mismatch" }));

        var result = await _mediator.Send(command);
        return ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteMealCommand(id));
        return ToActionResult(result);
    }

    private IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return Ok(ApiEnvelope.FromResult(result));

        if (result.ErrorType is ErrorType.NotFound)
            return NotFound(ApiEnvelope.FromResult(result));

        return BadRequest(ApiEnvelope.FromResult(result));
    }
}
