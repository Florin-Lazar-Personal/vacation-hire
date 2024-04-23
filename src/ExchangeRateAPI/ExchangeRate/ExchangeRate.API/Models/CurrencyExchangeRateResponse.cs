using ExchangeRate.API.Utils;

namespace ExchangeRate.API.Models
{
    public class CurrencyExchangeRateResponse
    {
        public CurrencyExchangeRateResponse(
            string sourceCurrency,
            string targetCurrency,
            DateTimeOffset timestamp,
            decimal quota,
            decimal amount,
            decimal result) 
        {
            SourceCurrency = Require.NotNull(sourceCurrency, nameof(sourceCurrency));
            TargetCurrency = Require.NotNull(targetCurrency, nameof(targetCurrency));
            Timestamp = Require.IsMoreRecentThan(timestamp, DateTimeOffset.MinValue, nameof(timestamp));
            Quota = Require.IsPositive(quota, nameof(quota));
            Amount = Require.IsPositive(amount, nameof(amount));
            Result = Require.IsPositive(result, nameof(result));
        }

        public string SourceCurrency { get; }

        public string TargetCurrency { get; }

        public DateTimeOffset Timestamp { get;  }

        public decimal Quota { get; }

        public decimal Amount { get; }

        public decimal Result { get; }
    }
}
