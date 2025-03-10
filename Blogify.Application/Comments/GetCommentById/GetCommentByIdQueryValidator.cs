﻿using Blogify.Domain.Comments;
using FluentValidation;

namespace Blogify.Application.Comments.GetCommentById;

internal sealed class GetCommentByIdQueryValidator : AbstractValidator<GetCommentByIdQuery>
{
    public GetCommentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(CommentError.EmptyCommentId.Code)
            .WithMessage(CommentError.EmptyCommentId.Description);
    }
}