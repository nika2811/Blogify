﻿using Blogify.Application.Categories.CreateCategory;
using FluentValidation.TestHelper;

namespace Blogify.Application.UnitTests.Categories.Create;

public class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator = new();

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var command = new CreateCategoryCommand("", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDescriptionIsEmpty()
    {
        // Arrange
        var command = new CreateCategoryCommand("TestCategory", "");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenInputsAreValid()
    {
        // Arrange
        var command = new CreateCategoryCommand("TestCategory", "Test Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}