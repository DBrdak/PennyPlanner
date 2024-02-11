using System.Reflection;
using CommonAbstractions.DB.Entities;
using CommonAbstractions.DB.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using Responses.DB;
using Serilog.Context;

namespace Domestica.Budget.Application.Behaviors
{
    public class DomainEventPublishBehavior<TRequest, TResponse, TEntity>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TEntity : IEntity
        where TResponse : Result<TEntity>
    {
        private readonly IPublisher _publisher;
        private readonly ILogger<DomainEventPublishBehavior<TRequest, TResponse, TEntity>> _logger;

        public DomainEventPublishBehavior(IPublisher publisher, ILogger<DomainEventPublishBehavior<TRequest, TResponse, TEntity>> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (response.IsFailure)
            {
                return response;
            }

            var domainEvents = response.Value.GetDomainEvents();

            if (domainEvents.Count < 1)
            {
                return response;
            }

            using (LogContext.PushProperty("DomainEvents", domainEvents, true))
            {
                LogDomainEventPublishingStart(domainEvents);

                foreach (var domainEvent in domainEvents)
                {
                    LogSingleDomainEventPublishingStart(domainEvent);

                    await _publisher.Publish(domainEvent, cancellationToken);

                    LogSingleDomainEventPublishingSuccess(domainEvent);
                }

                LogDomainEventPublishingSuccess(domainEvents);

                return response;
            }
        }

        private void LogDomainEventPublishingSuccess(IReadOnlyList<IDomainEvent> domainEvents)
        {
            _logger.LogInformation(
                $"The process of publishing domain events is completed, {domainEvents.Count} domain events were published");
        }

        private void LogSingleDomainEventPublishingSuccess(IDomainEvent domainEvent)
        {
            _logger.LogInformation("Domain event {DomainEvent} published", domainEvent.GetType().FullName);
        }

        private void LogSingleDomainEventPublishingStart(IDomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event {DomainEvent}", domainEvent.GetType().FullName);
        }

        private void LogDomainEventPublishingStart(IReadOnlyList<IDomainEvent> domainEvents)
        {
            _logger.LogInformation(
                $"The process of publishing domain events has started, {domainEvents.Count} domain events are up to be published");
        }
    }
}