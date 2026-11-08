using System.ComponentModel.DataAnnotations;

namespace CareerOs.Api.DTOs;

public class CreateGoalRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}