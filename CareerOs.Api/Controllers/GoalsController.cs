using CareerOs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using CareerOs.Api.Data;
using Microsoft.EntityFrameworkCore;
using CareerOs.Api.DTOs;

namespace CareerOs.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{

    private readonly CareerOsDbContext _db;

    public GoalsController(CareerOsDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Goal>>> GetGoals()
    {
        return Ok(await _db.Goals.ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Goal>> GetGoal(int id)
    {
        Goal? goal = await _db.Goals.FindAsync(id);

        if (goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    [HttpPost]
    public async Task<ActionResult<Goal>> CreateGoal([FromBody] CreateGoalRequest request)
    {
        Goal newGoal = new Goal
        {
            Title = request.Title,
            Description = request.Description ?? string.Empty,
            IsCompleted = false
        };

        _db.Goals.Add(newGoal);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetGoal),
            new { id = newGoal.Id },
            newGoal
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGoal(int id, [FromBody] UpdateGoalRequest request)
    {
        Goal? goal = await _db.Goals.FindAsync(id);

        if (goal == null)
        {
            return NotFound();
        }

        if (request.IsCompleted is bool isCompleted)
        {
            goal.Title = request.Title;
            goal.Description = request.Description ?? string.Empty;
            goal.IsCompleted = isCompleted;
        }
        else
        {
            return BadRequest("Completion status is required.");
        }
        
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        Goal? goal = await _db.Goals.FindAsync(id);

        if (goal == null)
        {
            return NotFound();
        }

        _db.Goals.Remove(goal);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}