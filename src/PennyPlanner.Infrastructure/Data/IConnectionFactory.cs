using Microsoft.Extensions.DependencyInjection;

namespace PennyPlanner.Infrastructure.Data
{
    internal interface IConnectionFactory
    {
        void Connect(IServiceCollection services);
    }
}
