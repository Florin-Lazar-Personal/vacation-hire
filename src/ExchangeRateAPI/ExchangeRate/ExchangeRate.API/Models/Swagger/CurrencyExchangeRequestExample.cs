using Swashbuckle.AspNetCore.Filters;

namespace ExchangeRate.API.Models.Swagger
{
    public class CurrencyExchangeRequestExample : IExamplesProvider<CurrencyExchangeRequest>
    {
        public CurrencyExchangeRequest GetExamples()
        {
            return new CurrencyExchangeRequest
            {
                SourceCurrency = "EUR",
                TargetCurrency = "USD",
                Amount = 50
            };
        }
    }
}
