﻿using Blogify.Domain.Abstractions;
using MediatR;

namespace Blogify.Application.Categories.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name, string Description) : IRequest<Result>;