using ExchangeRate.API.Models;
using ExchangeRate.API.Models.Swagger;
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
        [SwaggerRequestExample(typeof(CurrencyExchangeRequest), typeof(CurrencyExchangeRequestExample))]
        [HttpPost]
        public async Task<ActionResult<CurrencyExchangeRateResponse>> ExchangeAsync(
            [FromBody][Required] CurrencyExchangeRequest request)
        {
            await Task.CompletedTask;

            decimal quota = 1.5M;

            return Ok(new CurrencyExchangeRateResponse(
                request.SourceCurrency,
                request.TargetCurrency,
                DateTimeOffset.UtcNow,
                quota,
                request.Amount,
                request.Amount * quota));
        }
    }
}
