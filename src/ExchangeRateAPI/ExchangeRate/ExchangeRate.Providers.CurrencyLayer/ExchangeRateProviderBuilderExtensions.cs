using ExchangeRate.Providers.CurrencyLayer.Api;
using ExchangeRate.Providers.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace ExchangeRate.Providers.CurrencyLayer
{
    public static class ExchangeRateProviderBuilderExtensions
    {
        public static ExchangeRateProviderBuilder AddCurrencyLayer(
            this ExchangeRateProviderBuilder builder,
            Action<CurrencyLayerOptions> optionsSetup)
        {
            CurrencyLayerOptions options = new CurrencyLayerOptions();
            optionsSetup(options);

            builder.Services
                .AddRefitClient<ICurrencyLayerApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.BaseUrl))
                .AddHttpMessageHandler(_ => new CurrencyLayerAccessKeyHandler(options.AccessKey));

            return builder.AddExchangeRateProvider<CurrencyLayerExchangeRateProvider>();
        }
    }
}
