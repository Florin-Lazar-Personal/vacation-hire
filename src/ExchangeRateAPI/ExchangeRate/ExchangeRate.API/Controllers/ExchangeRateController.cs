using ExchangeRate.Abstractions.Services;
using ExchangeRate.API.Models;
using ExchangeRate.API.Models.Swagger;
using ExchangeRate.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/exchange-rates")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        { 
            _exchangeRateService = Require.ThatArgument(exchangeRateService, nameof(exchangeRateService))
                                          .IsNotNull();
        }

        [SwaggerRequestExample(typeof(CurrencyExchangeRequest), typeof(CurrencyExchangeRequestExample))]
        [HttpPost]
        public async Task<ActionResult<CurrencyExchangeRateResponse>> ExchangeAsync(
            [FromBody][Required] CurrencyExchangeRequest request)
        {
            // TODO: in next iteration add AutoMapper
            var sourceCurrencyAmount = new ExchangeRate.Models.CurrencyAmount(request.SourceCurrency, request.Amount);
            var targetCurrency = new ExchangeRate.Models.Currency(request.TargetCurrency);

            ExchangeRate.Models.CurrencyConversionResult result = await _exchangeRateService.ExchangeCurrencyAsync(
                sourceCurrencyAmount,
                targetCurrency);

            CurrencyExchangeRateResponse response = new CurrencyExchangeRateResponse(
                sourceCurrency: request.SourceCurrency,
                targetCurrency: request.TargetCurrency,
                timestamp: result.Timestamp,
                quota: result.ExchangeRate,
                amount: request.Amount,
                result: result.ExchangedAmount);

            return Ok(response);
        }
    }
}
