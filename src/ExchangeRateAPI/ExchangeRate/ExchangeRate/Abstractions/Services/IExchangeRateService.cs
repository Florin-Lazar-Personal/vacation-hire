using ExchangeRate.Models;
using System.Threading.Tasks;

namespace ExchangeRate.Abstractions.Services
{
    public interface IExchangeRateService
    {
        Task<CurrencyConversionResult> ExchangeCurrencyAsync(CurrencyAmount from, Currency to);
    }
}
