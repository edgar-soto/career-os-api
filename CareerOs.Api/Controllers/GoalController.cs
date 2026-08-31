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
}