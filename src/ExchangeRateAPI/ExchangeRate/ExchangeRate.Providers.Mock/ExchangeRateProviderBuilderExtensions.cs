using ExchangeRate.Providers.DependencyInjection;

namespace ExchangeRate.Providers.Mock
{
    public static class ExchangeRateProviderBuilderExtensions
    {
        public static ExchangeRateProviderBuilder AddMockProvider(this ExchangeRateProviderBuilder builder)
        {
            return builder.AddExchangeRateProvider<MockExchangeRateProvider>();
        }
    }
}
