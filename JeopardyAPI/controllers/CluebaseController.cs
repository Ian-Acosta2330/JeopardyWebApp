using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class CluebaseController : ControllerBase
{
    private readonly CluebaseService _cluebaseService;

    public CluebaseController(CluebaseService cluebaseService)
    {
        _cluebaseService = cluebaseService;
    }

    [HttpGet("questions")]
    public async Task<IActionResult> GetQuestions([FromQuery] int limit = 100, [FromQuery] string orderBy = "category", [FromQuery] string sort = "desc")
    {
        var questions = await _cluebaseService.GetQuestionsAsync(limit, orderBy, sort);
        if (questions == null)
        {
            return NotFound("Questions not found or API call failed.");
        }

        return Ok(questions);
    }
}
