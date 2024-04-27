using ExchangeRate.Validation;

namespace ExchangeRate.Models
{
    public class Currency
    {
        public Currency(string symbol)
        { 
            Symbol = Require.ThatArgument(symbol, nameof(symbol))
                            .IsNotNullOrWhiteSpace();
        }

        public string Symbol {  get; }
    }
}
