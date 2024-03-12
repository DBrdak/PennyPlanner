using CommonAbstractions.DB.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using ValidationException = FluentValidation.ValidationException;

namespace PennyPlanner.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            using (LogContext.PushProperty("Validation", context.RootContextData))
            {
                LogValidationStart();

                var validationFailures = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                var errors = GetValidationFailures(validationFailures);

                if (errors != null && errors.Any())
                {
                    LogValidationFailure(request, errors);
                    throw new ValidationException(errors);
                }

                LogValidationSuccess();
            }

            var response = await next();

            return response;
        }

        private static List<ValidationFailure> GetValidationFailures(ValidationResult[] validationFailures)
        {
            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(
                    validationFailure => new ValidationFailure(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage))
                .ToList();
            return errors;
        }

        private void LogValidationStart()
        {
            _logger.LogInformation("Validating request {Request}", typeof(TRequest).FullName);
        }

        private void LogValidationSuccess()
        {
            _logger.LogInformation("Request {Request} validation success", typeof(TRequest).FullName);
        }

        private void LogValidationFailure(TRequest request, List<ValidationFailure> errors)
        {
            var errorMessages = new List<string>();

            foreach (var validationFailure in errors)
            {
                errorMessages.Add(
                    $"{validationFailure.PropertyName} attempted with value {validationFailure.AttemptedValue} failed: {validationFailure.ErrorMessage}");
            }

            _logger.LogWarning($"{request} validation failed, {string.Join('\n', errorMessages)}");
        }
    }
}