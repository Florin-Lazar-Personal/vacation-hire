using ExchangeRate.Models;
using System.Threading.Tasks;

namespace ExchangeRate.Abstractions.Stores
{
    public interface IExchangeRateStore
    {
        Task<PersistedCurrencyConversionResult> PersistConversionResultAsync(CurrencyConversionResult conversionResult);
    }
}
