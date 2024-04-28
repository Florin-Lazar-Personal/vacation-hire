using ExchangeRate.Providers.CurrencyLayer.Models;
using Refit;
using System.Threading.Tasks;

namespace ExchangeRate.Providers.CurrencyLayer.Api
{
    public interface ICurrencyLayerApi
    {
        [Get("/convert")]
        Task<IApiResponse<CurrencyConversionResult>> GetCurrencyConversionResultAsync(string from, string to, decimal amount);
    }
}
