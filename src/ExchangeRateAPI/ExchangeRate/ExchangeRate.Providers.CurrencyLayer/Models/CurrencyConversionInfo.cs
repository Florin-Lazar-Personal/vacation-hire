using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ExchangeRate.Providers.CurrencyLayer.Models
{
    public class CurrencyConversionInfo
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("quote")]
        public decimal Quote { get; set; }
    }
}
