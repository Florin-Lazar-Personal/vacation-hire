using System;

namespace ExchangeRate.Providers.Exceptions
{
    public class ExchangeRateProviderSubscriptionPlanException : BaseExchangeRateProviderException
    {
        public ExchangeRateProviderSubscriptionPlanException(
            string provider)
            : this(provider, "The requested functionality is not available for your subscription plan, please contact provider's support.", null)
        {
        }

        public ExchangeRateProviderSubscriptionPlanException(
            string provider,
            string message)
            : this(provider, message, null)
        {
        }

        public ExchangeRateProviderSubscriptionPlanException(
            string provider,
            string message,
            Exception providerException)
            : base(provider, message, providerException)
        {
        }
    }
}
