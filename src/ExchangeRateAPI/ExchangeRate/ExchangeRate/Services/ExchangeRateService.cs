using ExchangeRate.Abstractions.Services;
using ExchangeRate.Abstractions.Stores;
using ExchangeRate.Exceptions;
using ExchangeRate.Models;
using ExchangeRate.Providers.Abstractions;
using ExchangeRate.Validation;
using System.Threading.Tasks;

namespace ExchangeRate.Services
{
    internal class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateProvider _provider;
        private readonly IExchangeRateStore _store;

        public ExchangeRateService(
            IExchangeRateProvider provider,
            IExchangeRateStore store)
        { 
            _provider = Require.ThatArgument(provider, nameof(provider)).IsNotNull();
            _store = Require.ThatArgument(store, nameof(store)).IsNotNull();
        }

        public async Task<CurrencyConversionResult> ExchangeCurrencyAsync(CurrencyAmount from, Currency to)
        {
            Require.ThatArgument(from, nameof(from)).IsNotNull();
            Require.ThatArgument(to, nameof(to)).IsNotNull();
            
            if (!Providers.Models.Currency.TryParse(from.Symbol, out Providers.Models.Currency fromCurrency))
            {
                throw new CurrencyNotSupportedException(from.Symbol);
            }

            if (!Providers.Models.Currency.TryParse(to.Symbol, out Providers.Models.Currency toCurrency))
            {
                throw new CurrencyNotSupportedException(to.Symbol);
            }

            // TODO: Catch provider exceptions.
            Providers.Models.CurrencyExchangeResult exchangeResult = await _provider.ExchangeCurrencyAsync(
                fromCurrency,
                from.Amount,
                toCurrency);

            // TODO: Later cand introduce AutoMapper for such model-to-model conversions.
            CurrencyConversionResult conversionResult = new CurrencyConversionResult(
                exchangeResult.Timestamp,
                from,
                to,
                exchangeResult.ExchangeRate,
                exchangeResult.ExchangedAmount);

            // store conversion for audit / historic reference.
            PersistedCurrencyConversionResult storedConversionResult = await _store.PersistConversionResultAsync(conversionResult);

            return storedConversionResult;
        }
    }
}
