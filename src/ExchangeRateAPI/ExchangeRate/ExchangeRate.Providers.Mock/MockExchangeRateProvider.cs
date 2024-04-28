using ExchangeRate.Providers.Abstractions;
using ExchangeRate.Providers.Models;
using System;
using System.Threading.Tasks;

namespace ExchangeRate.Providers.Mock
{
    internal class MockExchangeRateProvider : IExchangeRateProvider
    {
        public string Name => "MockProvider";

        public Task<CurrencyExchangeResult> ExchangeCurrencyAsync(Currency from, decimal amount, Currency to)
        {
            decimal exchangeRate = 1.5M;

            return Task.FromResult(new CurrencyExchangeResult(
                timestamp: DateTimeOffset.Now,
                sourceCurrency: from,
                sourceAmount: amount,
                targetCurrency: to,
                exchangeRate: exchangeRate,
                exchangedAmount: exchangeRate * amount));
        }
    }
}
