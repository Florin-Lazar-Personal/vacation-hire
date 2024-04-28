using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderAuthorizationException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderAuthorizationException(
            string provider)
            : this(provider, "There was an authorization issue while calling the exchange-rate provider.", null)
        {
        }

        public ExchangeRateProviderAuthorizationException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderAuthorizationException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
