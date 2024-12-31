using Blogify.Domain.Abstractions;
using MediatR;

namespace Blogify.Application.Comments.CreateComment;

public sealed record CreateCommentCommand(string Content, Guid AuthorId, Guid PostId) : IRequest<Result<Guid>>;