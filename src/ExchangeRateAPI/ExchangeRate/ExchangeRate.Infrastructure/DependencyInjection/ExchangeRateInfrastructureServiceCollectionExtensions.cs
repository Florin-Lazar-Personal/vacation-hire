using ExchangeRate.Abstractions.Stores;
using ExchangeRate.Infrastructure.Stores;
using ExchangeRate.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRate.Infrastructure.DependencyInjection
{
    public static class ExchangeRateInfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddExchangeRateInfrastructure(this IServiceCollection services)
        {
            Require.ThatArgument(services, nameof(services)).IsNotNull();

            // add infrastructure dependencies
            services.AddTransient<IExchangeRateStore, ExchangeRateStore>();

            return services;
        }
    }
}
