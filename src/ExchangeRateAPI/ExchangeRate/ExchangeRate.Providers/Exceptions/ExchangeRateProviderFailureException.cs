using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderFailureException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderFailureException(
            string provider,
            Exception providerException)
            : this(provider, "Failed to get exchange rate from provider.", providerException)
        {
        }

        public ExchangeRateProviderFailureException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
