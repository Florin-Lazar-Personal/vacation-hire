using Newtonsoft.Json;

namespace ExchangeRate.Providers.CurrencyLayer.Models
{
    public class CurrencyConversionResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public ErrorInfo Error { get; set; } = null;

        [JsonProperty("terms")]
        public string TermsAndConditionsUrl {  get; set; }

        [JsonProperty("privacy")]
        public string PrivacyPolicyUrl {  get; set; }

        [JsonProperty("query")]
        public CurrencyConversionQuery Query { get; set; } = new CurrencyConversionQuery();

        [JsonProperty("info")]
        public CurrencyConversionInfo Info { get; set; } = new CurrencyConversionInfo();

        [JsonProperty("result")]
        public decimal Result {  get; set; }
    }
}
