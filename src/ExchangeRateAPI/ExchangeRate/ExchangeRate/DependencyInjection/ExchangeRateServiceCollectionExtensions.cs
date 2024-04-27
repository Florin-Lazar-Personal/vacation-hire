using ExchangeRate.Abstractions.Services;
using ExchangeRate.Providers.DependencyInjection;
using ExchangeRate.Services;
using ExchangeRate.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExchangeRate.DependencyInjection
{
    public static class ExchangeRateServiceCollectionExtensions
    {
        public static IServiceCollection AddExchangeRate(
            this IServiceCollection services,
            Action<ExchangeRateProviderBuilder> providerSetup)
        {
            Require.ThatArgument(services, nameof(services)).IsNotNull();
            Require.ThatArgument(providerSetup, nameof(providerSetup)).IsNotNull();

            // add business logic dependencies
            services.AddTransient<IExchangeRateService, ExchangeRateService>();

            // add provider dependency injection setup
            ExchangeRateProviderBuilder builder = new ExchangeRateProviderBuilder(services);
            providerSetup(builder);

            return services;
        }
    }
}
