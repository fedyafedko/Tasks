using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Common.Utility;

namespace Tasks.Common.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection ConfigsAssembly(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<ConfigService> configService)
        {
            var builder = new ConfigService(services, configuration);
            configService(builder);

            return services;
        }
    }
}
