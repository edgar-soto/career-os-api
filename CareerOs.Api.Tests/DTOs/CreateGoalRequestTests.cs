using System.ComponentModel.DataAnnotations;
using CareerOs.Api.DTOs;
using Xunit;

namespace CareerOs.Api.Tests.DTOs;

public class CreateGoalRequestTests
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
    public void MissingTitle_IsInvalid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Description = "A goal without a title."
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.NotEmpty(errors);

        Assert.Contains(
            errors,
            error => error.MemberNames.Contains(
                nameof(CreateGoalRequest.Title)
            )
        );
    }

    [Fact]
    public void MissingDescription_IsValid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = "Goal without a description"
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void ValidRequest_PassesValidation()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = "Learn Automated Testing",
            Description = "Practice writing tests with xUnit."
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void TitleLongerThan120Characters_IsInvalid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = new string('A', 121),
            Description = "A goal exceeding title character length."
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.NotEmpty(errors);

        Assert.Contains(
            errors,
            error => error.MemberNames.Contains(
                nameof(CreateGoalRequest.Title)
            )
        );
    }

    [Fact]
    public void TitleExactly120Characters_IsValid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = new string('A', 120),
            Description = "A goal with max title character length."
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void DescriptionLongerThan500Characters_IsInvalid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = "A goal exceeding description character length.",
            Description = new string('A', 501)
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.NotEmpty(errors);

        Assert.Contains(
            errors,
            error => error.MemberNames.Contains(
                nameof(CreateGoalRequest.Description)
            )
        );
    }

    [Fact]
    public void DescriptionExactly500Characters_IsValid()
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = "A goal with maximum description length.",
            Description = new string('A', 500)
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.Empty(errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void EmptyOrWhitespaceTitle_IsInvalid(string title)
    {
        // Arrange
        var request = new CreateGoalRequest
        {
            Title = title
        };

        // Act
        var errors = Validate(request);

        // Assert
        Assert.NotEmpty(errors);

        Assert.Contains(
            errors,
            error => error.MemberNames.Contains(
                nameof(CreateGoalRequest.Title)
            )
        );
    }
}