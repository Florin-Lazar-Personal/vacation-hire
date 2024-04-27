using ExchangeRate.Providers.Models;
using System.Threading.Tasks;

namespace ExchangeRate.Providers.Abstractions
{
    public interface IExchangeRateProvider
    {
        string Name { get; }

        Task<CurrencyExchangeResult> ExchangeCurrencyAsync(Currency from, decimal amount, Currency to);
    }
}
