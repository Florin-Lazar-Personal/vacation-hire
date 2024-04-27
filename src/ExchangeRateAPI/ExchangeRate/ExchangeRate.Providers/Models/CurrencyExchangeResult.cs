using System;

namespace ExchangeRate.Providers.Models
{
    public class CurrencyExchangeResult
    {
        private static readonly DateTimeOffset MIN_TIMESTAMP_UTC = new DateTimeOffset(new DateTime(2024, 1, 1), TimeSpan.FromHours(0));

        public CurrencyExchangeResult(
            DateTimeOffset timestamp,
            Currency sourceCurrency,
            decimal sourceAmount,
            Currency targetCurrency,
            decimal exchangeRate,
            decimal exchangedAmount)
        {
            if (timestamp < MIN_TIMESTAMP_UTC)
            {
                throw new ArgumentException(
                    message: $"Timestamp must be a value greater than '{MIN_TIMESTAMP_UTC:O}'.",
                    paramName: nameof(sourceAmount));
            }

            Timestamp = timestamp;

            SourceCurrency = sourceCurrency ?? throw new ArgumentNullException(nameof(sourceCurrency));
            TargetCurrency = targetCurrency ?? throw new ArgumentNullException( nameof(targetCurrency));

            if(sourceAmount < 0)
            {
                throw new ArgumentException(
                    message: "Source amount must be a positive value.",
                    paramName: nameof(sourceAmount));  
            }

            if (exchangeRate < 0)
            {
                throw new ArgumentException(
                    message: "Exchange rate must be a positive value.",
                    paramName: nameof(sourceAmount));
            }

            if (exchangedAmount < 0)
            {
                throw new ArgumentException(
                    message: "Exchanged amount must be a positive value.",
                    paramName: nameof(sourceAmount));
            }

            SourceAmount = sourceAmount;
            ExchangeRate = exchangeRate;
            ExchangedAmount = exchangedAmount;
        }

        public Currency SourceCurrency { get; }

        public decimal SourceAmount { get; }

        public Currency TargetCurrency { get; }

        public DateTimeOffset Timestamp { get;  }

        public decimal ExchangeRate { get; }

        public decimal ExchangedAmount { get; }
    }
}
