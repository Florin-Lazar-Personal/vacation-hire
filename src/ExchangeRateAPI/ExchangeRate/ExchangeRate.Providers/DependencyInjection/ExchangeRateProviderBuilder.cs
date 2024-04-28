using ExchangeRate.Providers.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExchangeRate.Providers.DependencyInjection
{
    public class ExchangeRateProviderBuilder
    {
        public ExchangeRateProviderBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public ExchangeRateProviderBuilder AddExchangeRateProvider<T>()
            where T : class, IExchangeRateProvider
        {
            Services.AddScoped<IExchangeRateProvider, T>();

            return this;
        }

        public ExchangeRateProviderBuilder AddExchangeRateProvider<T>(Func<IServiceProvider, T> factory)
            where T : class, IExchangeRateProvider
        {
            Services.AddScoped<IExchangeRateProvider, T>(factory);

            return this;
        }
    }
}
