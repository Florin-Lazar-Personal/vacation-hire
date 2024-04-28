using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderResourceNotFoundException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderResourceNotFoundException(
            string provider)
            : this(provider, "The requested resource doesn't exist.", null)
        {
        }

        public ExchangeRateProviderResourceNotFoundException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderResourceNotFoundException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
