using System.ComponentModel.DataAnnotations;
using CareerOs.Api.DTOs;
using Xunit;

namespace CareerOs.Api.Tests.DTOs;

public class UpdateGoalRequestTests
{
    private static List<ValidationResult> Validate(object request)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(request);

        Validator.TryValidateObject(
            request,
            context,
            results,
            validateAllProperties: true
        );

        return results;
    }

    [Fact]
    public void MissingCompletionStatus_IsInvalid()
    {
        // Arrange
        var request = new UpdateGoalRequest
        {
            Title = "A goal with no completion status",
            IsCompleted = null
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.NotEmpty(errors);

        Assert.Contains(
            errors,
            error => error.MemberNames.Contains(
                nameof(UpdateGoalRequest.IsCompleted)
            )
        );
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ProvidedCompletionStatus_IsValid(bool isCompleted)
    {
        // Arrange
        var request = new UpdateGoalRequest
        {
            Title = "A goal",
            IsCompleted = isCompleted
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.Empty(errors);
    }
}