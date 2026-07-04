using MediatR;
using Microsoft.AspNetCore.Mvc;
using Elevate.Nutrition.Api.Dtos.Responses;
using Elevate.Nutrition.Application.Features.MealPlans.Commands.AddMealPlanItem;
using Elevate.Nutrition.Application.Features.MealPlans.Commands.CreateMealPlan;
using Elevate.Nutrition.Application.Features.MealPlans.Commands.DeleteMealPlan;
using Elevate.Nutrition.Application.Features.MealPlans.Commands.RemoveMealPlanItem;
using Elevate.Nutrition.Application.Features.MealPlans.Commands.UpdateMealPlan;
using Elevate.Nutrition.Application.Features.MealPlans.Queries.GetAllMealPlans;
using Elevate.Nutrition.Application.Features.MealPlans.Queries.GetMealPlanById;
using Elevate.Nutrition.Domain.Common;

namespace Elevate.Nutrition.Api.Controllers;

[Route("api/nutrition/mealplans")]
[ApiController]
public class MealPlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public MealPlansController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllMealPlansQuery());
        return ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMealPlanByIdQuery(id));
        return ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMealPlanCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return ToActionResult(result);

        return CreatedAtAction(nameof(GetById), new { id = ((Result<int>)result).Value },
            ApiEnvelope.FromResult(result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMealPlanCommand command)
    {
        if (id != command.Id)
            return BadRequest(ApiEnvelope.Failure(new List<string> { "Route id and body id mismatch" }));

        var result = await _mediator.Send(command);
        return ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteMealPlanCommand(id));
        return ToActionResult(result);
    }

    [HttpPost("{id:int}/items")]
    public async Task<IActionResult> AddItem(int id, [FromBody] AddMealPlanItemCommand command)
    {
        if (id != command.MealPlanId)
            return BadRequest(ApiEnvelope.Failure(new List<string> { "Route id and body MealPlanId mismatch" }));

        var result = await _mediator.Send(command);
        return ToActionResult(result);
    }

    [HttpDelete("{id:int}/items/{itemId:int}")]
    public async Task<IActionResult> RemoveItem(int id, int itemId)
    {
        var result = await _mediator.Send(new RemoveMealPlanItemCommand(id, itemId));
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
