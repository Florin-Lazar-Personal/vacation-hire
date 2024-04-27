using System.Collections.Generic;

namespace ExchangeRate.Providers.Models
{
    public partial class Currency
    {
        public static readonly IReadOnlyList<string>
            AllSymbols = new[]
        {
            "EUR",
            "USD",
            "RON"
        };
    }
}
