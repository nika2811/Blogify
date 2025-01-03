﻿namespace Blogify.Domain.Comments;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);
    Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Comment>> GetByPostIdAsync(Guid postId, CancellationToken cancellationToken);
}