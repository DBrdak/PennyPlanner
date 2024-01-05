using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Budgetify.Application
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
