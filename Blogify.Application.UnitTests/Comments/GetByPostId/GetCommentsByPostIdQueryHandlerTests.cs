﻿using Blogify.Application.Comments.GetCommentsByPostId;
using Blogify.Domain.Comments;
using FluentAssertions;
using NSubstitute;

namespace Blogify.Application.UnitTests.Comments.GetByPostId;

public class GetCommentsByPostIdQueryHandlerTests
{
    private readonly ICommentRepository _commentRepositoryMock;
    private readonly GetCommentsByPostIdQueryHandler _handler;

    public GetCommentsByPostIdQueryHandlerTests()
    {
        _commentRepositoryMock = Substitute.For<ICommentRepository>();
        _handler = new GetCommentsByPostIdQueryHandler(_commentRepositoryMock);
    }

    [Fact]
    public async Task Handle_CommentsExistForPostId_ReturnsListOfCommentResponses()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var comments = new List<Comment>
        {
            Comment.Create("Content 1", Guid.NewGuid(), postId).Value,
            Comment.Create("Content 2", Guid.NewGuid(), postId).Value
        };
        _commentRepositoryMock.GetByPostIdAsync(postId, Arg.Any<CancellationToken>()).Returns(comments);

        var query = new GetCommentsByPostIdQuery(postId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value.Should().Contain(c => c.Content == "Content 1" && c.PostId == postId);
        result.Value.Should().Contain(c => c.Content == "Content 2" && c.PostId == postId);
        await _commentRepositoryMock.Received(1).GetByPostIdAsync(postId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NoCommentsExistForPostId_ReturnsEmptyList()
    {
        // Arrange
        var postId = Guid.NewGuid();
        _commentRepositoryMock.GetByPostIdAsync(postId, Arg.Any<CancellationToken>())
            .Returns(new List<Comment>());

        var query = new GetCommentsByPostIdQuery(postId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
        await _commentRepositoryMock.Received(1).GetByPostIdAsync(postId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_PostIdIsEmpty_ReturnsValidationError()
    {
        // Arrange
        var query = new GetCommentsByPostIdQuery(Guid.Empty);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CommentError.EmptyPostId);
        await _commentRepositoryMock.DidNotReceive().GetByPostIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_CancellationRequested_ThrowsOperationCanceledException()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var cancellationToken = new CancellationToken(true);

        var query = new GetCommentsByPostIdQuery(postId);

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(query, cancellationToken));
        await _commentRepositoryMock.DidNotReceive().GetByPostIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}