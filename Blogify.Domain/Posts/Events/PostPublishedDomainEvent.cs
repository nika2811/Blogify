﻿using Blogify.Domain.Abstractions;

namespace Blogify.Domain.Posts.Events;

public sealed record PostPublishedDomainEvent(Guid PostId) : IDomainEvent;
