using System;

namespace ExchangeRate.Providers.Exceptions
{
    public abstract class BaseExchangeRateProviderException : Exception
    {
        protected BaseExchangeRateProviderException(string provider) 
            : this(provider, "The exchange-rate provider returned an unspecified error.")
        {
            
        }

        protected BaseExchangeRateProviderException(string provider, string message)
            : this(provider, message, null)
        {
        }

        protected BaseExchangeRateProviderException(string provider, string message, Exception innerException)
            : base(message, innerException)
        {
            Provider = provider;
        }

        public string Provider { get; }
    }
}
