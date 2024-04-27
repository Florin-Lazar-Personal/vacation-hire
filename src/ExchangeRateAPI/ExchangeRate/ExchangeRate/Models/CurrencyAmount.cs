using ExchangeRate.Validation;

namespace ExchangeRate.Models
{
    public class CurrencyAmount : Currency
    {
        public CurrencyAmount(string symbol, decimal amount)
            : base(symbol)
        {
            Amount = Require.ThatArgument(amount, nameof(amount))
                            .SatisfiesCondition(x => x >= 0, "Amount must be a positive decimal value.");
        }

        public decimal Amount { get; set; }
    }
}
