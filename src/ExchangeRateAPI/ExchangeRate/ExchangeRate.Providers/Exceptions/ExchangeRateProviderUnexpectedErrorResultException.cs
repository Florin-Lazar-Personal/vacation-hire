using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderUnexpectedErrorResultException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderUnexpectedErrorResultException(
            string provider)
            : this(provider, "Your request returned an unexpected error.", null)
        {
        }

        public ExchangeRateProviderUnexpectedErrorResultException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderUnexpectedErrorResultException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
