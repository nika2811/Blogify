﻿using Blogify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Blogify.Application.Abstractions.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            logger.LogInformation("Executing request {RequestName}", requestName);

            var result = await next();

            if (result.IsSuccess)
                logger.LogInformation("Request {RequestName} processed successfully", requestName);
            else
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Request {RequestName} processed with error", requestName);
                }

            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Request {RequestName} processing failed", requestName);

            throw;
        }
    }
}