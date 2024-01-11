using Domestica.Budget.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Domestica.Budget.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddMediatR(
                config =>
                {
                    config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
                    config.AddOpenBehavior(typeof(DomainEventPublishBehavior<,>));
                    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                });
            
            return services;
        }
    }
}
