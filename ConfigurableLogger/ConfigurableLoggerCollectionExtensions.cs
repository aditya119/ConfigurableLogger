using ConfigurableLogger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurableLoggerCollectionExtensions
    {
        public static IServiceCollection AddConfigurableLogger(
             this IServiceCollection services, LogConfigModel logConfig)
        {
            services.AddSingleton(logConfig);

            services.AddScoped<ILoggingService, LoggingService>();

            return services;
        }
    }
}
