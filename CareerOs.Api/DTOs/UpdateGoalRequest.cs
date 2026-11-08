using System.ComponentModel.DataAnnotations;

namespace CareerOs.Api.DTOs;

public class UpdateGoalRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Completion status is required.")]
    public bool? IsCompleted { get; set; }
}