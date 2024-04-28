using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderRateLimitException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderRateLimitException(
            string provider)
            : this(provider, "The rate limit for this action has been exceeded, please try again later.", null)
        {
        }

        public ExchangeRateProviderRateLimitException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderRateLimitException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
