using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderRequestValidationException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderRequestValidationException(
            string provider)
            : this(provider, "There were validation error(s) for your request.", null)
        {
        }

        public ExchangeRateProviderRequestValidationException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderRequestValidationException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
