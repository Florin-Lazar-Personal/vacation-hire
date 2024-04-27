using ExchangeRate.Abstractions.Stores;
using ExchangeRate.Models;
using System;
using System.Threading.Tasks;

namespace ExchangeRate.Infrastructure.Stores
{
    internal class ExchangeRateStore : IExchangeRateStore
    {
        public async Task<PersistedCurrencyConversionResult> PersistConversionResultAsync(CurrencyConversionResult conversionResult)
        {
            // TODO: add a real DB here
            await Task.CompletedTask;

            return new PersistedCurrencyConversionResult(
                id: Guid.NewGuid().ToString("D"),
                timestamp: conversionResult.Timestamp,
                sourceCurrency: conversionResult.SourceCurrency,
                targetCurrency: conversionResult.TargetCurrency,
                exchangeRate: conversionResult.ExchangeRate,
                exchangedAmount: conversionResult.ExchangedAmount);
        }
    }
}
