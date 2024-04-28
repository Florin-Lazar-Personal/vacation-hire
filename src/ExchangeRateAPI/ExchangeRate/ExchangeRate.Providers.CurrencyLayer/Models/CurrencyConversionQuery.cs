using Newtonsoft.Json;

namespace ExchangeRate.Providers.CurrencyLayer.Models
{
    public class CurrencyConversionQuery
    {
        [JsonProperty("from")]
        public string SourceCurrencySymbol { get; set; }

        [JsonProperty("to")]
        public string TargetCurrencySymbol { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
