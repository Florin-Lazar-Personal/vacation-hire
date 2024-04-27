using ExchangeRate.Providers.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExchangeRate.Providers.DependencyInjection
{
    public class ExchangeRateProviderBuilder
    {
        private readonly IServiceCollection _services;

        public ExchangeRateProviderBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public ExchangeRateProviderBuilder AddExchangeRateProvider<T>()
            where T : class, IExchangeRateProvider
        {
            _services.AddScoped<IExchangeRateProvider, T>();

            return this;
        }

        public ExchangeRateProviderBuilder AddExchangeRateProvider<T>(Func<IServiceProvider, T> factory)
            where T : class, IExchangeRateProvider
        {
            _services.AddScoped<IExchangeRateProvider, T>(factory);

            return this;
        }
    }
}
