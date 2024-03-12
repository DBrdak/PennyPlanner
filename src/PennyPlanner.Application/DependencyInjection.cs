using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PennyPlanner.Application.Behaviors;

namespace PennyPlanner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);
            
            services.AddMediatR(
                config =>
                {
                    config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
                    config.AddOpenBehavior(typeof(DomainEventPublishBehavior<,>));
                    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                    config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
                    config.AddOpenBehavior(typeof(CacheInvalidationBehavior<,>));
                });
            
            return services;
        }
    }
}
