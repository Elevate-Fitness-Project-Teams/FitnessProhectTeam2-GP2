using Microsoft.AspNetCore.Mvc;
using Elevate.Fitness.Services;

namespace Elevate.Fitness.Controllers;

[Route("api/calorie-target")]
[ApiController]
public class CalorieTargetController : ControllerBase
{
    private readonly CalorieTargetService _service;

    public CalorieTargetController(CalorieTargetService service) => _service = service;

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var target = await _service.GetByUserIdAsync(userId);

        if (target is null)
            return NotFound(new { isSuccess = false, error = "Calorie target not found for user" });

        return Ok(new { isSuccess = true, data = target });
    }
}
