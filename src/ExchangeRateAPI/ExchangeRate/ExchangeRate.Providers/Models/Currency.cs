using System;
using System.Linq;

namespace ExchangeRate.Providers.Models
{
    public partial class Currency
    {
        private Currency(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }

        public static Currency Parse(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentNullException(
                    message: "Unspecified currency symbol.",
                    paramName: nameof(symbol));
            }

            bool canParse = TryParse(symbol, out Currency result);
            if (!canParse)
            {
                throw new ArgumentException(
                    message: $"Not supported currency symbol '{symbol}'.",
                    paramName: nameof(symbol));
            }

            return result;
        }

        public static bool TryParse(string symbol, out Currency currency)
        {
            currency = null;

            if (string.IsNullOrWhiteSpace(symbol))
            {
                return false;
            }

            string supportedSymbol = AllSymbols.FirstOrDefault(sym => string.Equals(sym, symbol, StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrEmpty(supportedSymbol))
            {
                return false;
            }

            currency = new Currency(supportedSymbol);
            return true;
        }
    }
}
