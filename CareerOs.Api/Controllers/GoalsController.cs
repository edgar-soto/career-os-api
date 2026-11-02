using CareerOs.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CareerOs.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private static readonly List<Goal> Goals = new List<Goal>
    {
        new Goal
        {
            Id = 1,
            Title = "Build Career OS API",
            Description = "Build the first version of Career OS using ASP.NET Core and PostgreSQL.",
            IsCompleted = false
        },
        new Goal
        {
            Id = 2,
            Title = "Improve backend development skills",
            Description = "Build practical experience with APIs, databases, and backend architecture.",
            IsCompleted = false
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Goal>> GetGoals()
    {
        return Ok(Goals);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Goal> GetGoal(int id)
    {
        Goal? goal = Goals.FirstOrDefault(g => g.Id == id);

        if (goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    [HttpPost]
    public ActionResult<Goal> CreateGoal([FromBody] Goal newGoal)
    {
        if (string.IsNullOrWhiteSpace(newGoal.Title))
        {
            return BadRequest("Title is required.");
        }

        newGoal.Id = Goals.Count == 0 ? 1 : Goals.Max(g => g.Id) + 1;

        newGoal.IsCompleted = false;

        Goals.Add(newGoal);

        return CreatedAtAction(
            nameof(GetGoal),
            new { id = newGoal.Id },
            newGoal
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateGoal(int id, [FromBody] Goal updatedGoal)
    {
        Goal? goal = Goals.FirstOrDefault(g => g.Id == id);

        if (goal == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(updatedGoal.Title))
        {
            return BadRequest("Title is required.");
        }

        goal.Title = updatedGoal.Title;
        goal.Description = updatedGoal.Description;
        goal.IsCompleted = updatedGoal.IsCompleted;

        return NoContent();
    }
}