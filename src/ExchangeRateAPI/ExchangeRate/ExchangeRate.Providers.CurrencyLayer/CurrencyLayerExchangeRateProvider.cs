using ExchangeRate.Providers.Abstractions;
using ExchangeRate.Providers.CurrencyLayer.Api;
using ExchangeRate.Providers.Exceptions;
using ExchangeRate.Providers.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExchangeRate.Providers.CurrencyLayer
{
    internal class CurrencyLayerExchangeRateProvider : IExchangeRateProvider
    {
        private readonly ICurrencyLayerApi _currencyLayerApi;
        private readonly ILogger<CurrencyLayerExchangeRateProvider> _logger;

        public CurrencyLayerExchangeRateProvider(
            ICurrencyLayerApi currencyLayerApi,
            ILogger<CurrencyLayerExchangeRateProvider> logger)
        {
            _currencyLayerApi = currencyLayerApi;
            _logger = logger;
        }

        public string Name => "CurrencyLayer";

        public async Task<CurrencyExchangeResult> ExchangeCurrencyAsync(Currency from, decimal amount, Currency to)
        {
            Refit.IApiResponse<Models.CurrencyConversionResult> result = await _currencyLayerApi.GetCurrencyConversionResultAsync(
                from.Symbol,
                to.Symbol,
                amount);

            if (result.IsSuccessStatusCode)
            {
                // we have a successfull (HTTP 200) response
                // that may not mean success, though
                if (!result.Content.Success)
                {
                    Models.ErrorInfo errorInfo = result.Content.Error ?? new Models.ErrorInfo 
                    {
                        Code = -1,
                        Details = "Unable to get error details from the exchange-rate provider."
                    };
                    
                    _logger.LogError(
                        "The exchange rate provider returned an error response: ({Code}) {Details}",
                        errorInfo.Code,
                        errorInfo.Details);

                    throw GetExceptionFromError(errorInfo);
                }

                return new CurrencyExchangeResult(
                    timestamp: result.Content.Info.Timestamp,
                    sourceCurrency: from,
                    sourceAmount: amount,
                    targetCurrency: to,
                    exchangeRate: result.Content.Info.Quote,
                    exchangedAmount: result.Content.Result);
            }
            else
            {
                _logger.LogError(result.Error, result.Error.Content);

                throw new ExchangeRateProviderFailureException(Name, result.Error);
            }
        }

        private Exception GetExceptionFromError(Models.ErrorInfo errorInfo)
        {
            switch (errorInfo.Code)
            {
                case 101: // User did not supply an access key or supplied an invalid access key.
                    return new ExchangeRateProviderAuthorizationException(
                        Name,
                        errorInfo.Details);

                case 102: // The user's account is not active. User will be prompted to get in touch with Customer Support.
                    return new ExchangeRateProviderAuthorizationException(
                       Name,
                       errorInfo.Details);

                case 103: // User requested a non-existent API function.
                    return new ExchangeRateProviderResourceNotFoundException(
                        Name,
                        errorInfo.Details);

                case 104: // User has reached or exceeded his subscription plan's monthly API request allowance.
                    return new ExchangeRateProviderRateLimitException(
                        Name,
                        errorInfo.Details);

                case 105: // The user's current subscription plan does not support the requested API function.
                    return new ExchangeRateProviderSubscriptionPlanException(
                        Name,
                        errorInfo.Details);

                case 106: // The user's query did not return any results
                    return new ExchangeRateProviderNoResultsException(
                        Name,
                        errorInfo.Details);
                
                case 201: // User entered an invalid Source Currency.
                case 202: // User entered one or more invalid currency codes.
                case 301: // User did not specify a date. [historical]
                case 302: // User entered an invalid date. [historical, convert]
                case 401: // User entered an invalid "from" property. [convert]
                case 402: // User entered an invalid "to" property. [convert]
                case 403: // User entered no or an invalid "amount" property. [convert]
                case 501: // User did not specify a Time-Frame. [timeframe, convert].
                case 502: // User entered an invalid "start_date" property. [timeframe, convert].
                case 503: // User entered an invalid "end_date" property. [timeframe, convert].
                case 504: // User entered an invalid Time-Frame. [timeframe, convert]
                case 505: // The Time-Frame specified by the user is too long - exceeding 365 days. [timeframe]
                    return new ExchangeRateProviderRequestValidationException(
                        Name,
                        errorInfo.Details);

                default:
                    return new ExchangeRateProviderUnexpectedErrorResultException(
                        Name,
                        $"({errorInfo.Code}) {errorInfo.Details}");
            }
        }
    }
}
