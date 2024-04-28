using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderNoResultsException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderNoResultsException(
            string provider)
            : this(provider, "Your request returned no results.", null)
        {
        }

        public ExchangeRateProviderNoResultsException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderNoResultsException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
