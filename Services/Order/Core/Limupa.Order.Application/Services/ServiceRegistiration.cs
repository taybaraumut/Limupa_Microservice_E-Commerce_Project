using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limupa.Order.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(config=>config.RegisterServicesFromAssemblies(typeof(ServiceRegistiration).Assembly));
        }
    }
}
