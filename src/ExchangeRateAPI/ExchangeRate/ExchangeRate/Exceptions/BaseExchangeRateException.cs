using System;

namespace ExchangeRate.Exceptions
{
    public abstract class BaseExchangeRateException : Exception
    {
        protected BaseExchangeRateException(string message)
            : base(message)
        {
        }

        protected BaseExchangeRateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public abstract ErrorTypes ErrorType { get; }
    }
}
