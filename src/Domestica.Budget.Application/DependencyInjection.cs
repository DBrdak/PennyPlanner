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

                });
            
            return services;
        }
    }
}
