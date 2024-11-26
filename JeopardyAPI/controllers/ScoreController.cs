using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase {
    [HttpPost]
    public IActionResult SaveScore([FromBody] Score score) {
        ScoreService.SaveScore(score.Player, score.Value);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetScores() {
        List<Score> scores = ScoreService.GetScores();
        return Ok(scores);
    }
}
