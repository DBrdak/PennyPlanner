﻿using Exceptions.DB;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Responses.DB;
using Serilog.Context;
using static Domestica.Budget.API.Middlewares.ExceptionMiddleware;

namespace Domestica.Budget.API.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var exceptionDetails = GetExceptionDetails(exception);

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Title = exceptionDetails.Title,
                    Detail = exceptionDetails.Detail,
                };

                if (exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exceptionDetails.Errors;
                }

                LogException(exceptionDetails, exception);

                context.Response.StatusCode = exceptionDetails.Status;

                if (exceptionDetails.Errors is null)
                    await context.Response.WriteAsJsonAsync(Result.Failure(Error.Exception(exceptionDetails.Title)));
                else
                    await context.Response.WriteAsJsonAsync(
                        Result.Failure(
                            Error.ValidationError(exceptionDetails.Errors.Select(e => e.ToString())!)));
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    validationException.GetType().Name,
                    "Validation error",
                    "One or more validation errors has occurred",
                    validationException.Errors),
                DomainException domainException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    domainException.GetType().Name,
                    $"{domainException.Type.Name} Domain error",
                    domainException.Message,
                    null),
                ApplicationException applicationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    applicationException.GetType().Name,
                    "Application error",
                    applicationException.Message,
                    null),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server error",
                    "An unexpected error has occurred",
                    null)
            };
        }

        private void LogException(ExceptionDetails details, Exception exception)
        {
            using (LogContext.PushProperty("Exception", details.Type, true))
            {
                _logger.LogError(exception, "{Exception} occurred: {Message}", details.Title, exception.Message);
            }
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors);
    }
}