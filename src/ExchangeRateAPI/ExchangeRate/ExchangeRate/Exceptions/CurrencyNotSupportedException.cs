using System;

namespace ExchangeRate.Exceptions
{
    public class CurrencyNotSupportedException : BaseExchangeRateException
    {
        public CurrencyNotSupportedException(string symbol)
            : this(symbol, $"Symbol '{symbol}' doesn't represent a supported currency.", null)
        {
        }

        public CurrencyNotSupportedException(string symbol, string message)
            : this (symbol, message, null)
        {
        }

        public CurrencyNotSupportedException(string symbol, string message, Exception innerException) 
            : base(message, innerException)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }

        public override ErrorTypes ErrorType => ErrorTypes.RequestValidation;
    }
}
