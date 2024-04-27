using ExchangeRate.Validation;
using System;

namespace ExchangeRate.Models
{
    public class PersistedCurrencyConversionResult : CurrencyConversionResult
    {
        public PersistedCurrencyConversionResult(
            string id,
            DateTimeOffset timestamp,
            CurrencyAmount sourceCurrency,
            Currency targetCurrency,
            decimal exchangeRate,
            decimal exchangedAmount)
            : base (timestamp, sourceCurrency, targetCurrency, exchangeRate, exchangedAmount)
        {
            Id = Require.ThatArgument(id, nameof(id)).IsNotNullOrWhiteSpace(); ;
        }

        public string Id { get; }
    }
}
