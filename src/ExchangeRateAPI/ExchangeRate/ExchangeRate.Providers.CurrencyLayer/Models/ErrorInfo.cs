using Newtonsoft.Json;

namespace ExchangeRate.Providers.CurrencyLayer.Models
{
    public class ErrorInfo
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("info")]
        public string Details { get; set; }
    }
}
