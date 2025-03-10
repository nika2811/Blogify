﻿using Blogify.Application.Categories.UpdateCategory;
using FluentValidation.TestHelper;

namespace Blogify.Application.UnitTests.Categories.Update;

public class UpdateCategoryCommandValidatorTests
{
    private readonly UpdateCategoryCommandValidator _validator = new();

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsTooLong()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), new string('a', 101), "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDescriptionIsEmpty()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "TestCategory", "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDescriptionIsTooLong()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "TestCategory", new string('a', 501));

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenInputsAreValid()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "TestCategory", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_ShouldHaveError_WhenNameIsNullOrEmpty(string name)
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), name, "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDescriptionExceedsMaxLength()
    {
        // Arrange
        var command = new UpdateCategoryCommand(Guid.NewGuid(), "TestCategory",
            new string('a', CategoryConstraints.DescriptionMaxLength + 1));

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}