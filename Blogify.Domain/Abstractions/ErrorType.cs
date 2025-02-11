﻿namespace Blogify.Domain.Abstractions;

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4,
    Unexpected = 5,
    AuthenticationFailed = 6
}