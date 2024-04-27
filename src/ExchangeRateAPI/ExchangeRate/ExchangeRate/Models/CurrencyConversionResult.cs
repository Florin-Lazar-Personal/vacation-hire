using ExchangeRate.Validation;
using System;

namespace ExchangeRate.Models
{
    public class CurrencyConversionResult
    {
        public CurrencyConversionResult(
            DateTimeOffset timestamp,
            CurrencyAmount sourceCurrency,
            Currency targetCurrency,
            decimal exchangeRate,
            decimal exchangedAmount)
        {
            Timestamp = Require.ThatArgument(timestamp, nameof(timestamp))
                               .SatisfiesCondition(
                                    x => x > DateTimeOffset.MinValue,
                                    "Timestamp must be a valid date/time value.");

            SourceCurrency = Require.ThatArgument(sourceCurrency, nameof(sourceCurrency))
                                    .IsNotNull();

            TargetCurrency = Require.ThatArgument(targetCurrency, nameof(sourceCurrency))
                                    .IsNotNull();

            ExchangeRate = Require.ThatArgument(exchangeRate, nameof(exchangeRate))
                                  .SatisfiesCondition(
                                        x => x >= 0,
                                        "Exchange rate must be a positive decimal value.");

            ExchangedAmount = Require.ThatArgument(exchangedAmount, nameof(exchangedAmount))
                                     .SatisfiesCondition(
                                            x => x >= 0,
                                            "Exchanged amount must be a positive decimal value.");
        }

        public CurrencyAmount SourceCurrency
        {
            get;
        }

        public Currency TargetCurrency
        {
            get;
        }

        public decimal ExchangeRate
        {
            get;
        }

        public DateTimeOffset Timestamp
        {
            get;
        }

        public decimal ExchangedAmount
        {
            get;
        }
    }
}
