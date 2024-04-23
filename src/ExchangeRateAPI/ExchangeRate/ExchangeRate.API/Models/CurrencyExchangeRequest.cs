using System.ComponentModel.DataAnnotations;

namespace ExchangeRate.API.Models
{
    public class CurrencyExchangeRequest
    {
        [Required]
        public string SourceCurrency {  get; set; } = string.Empty;

        [Required]
        public string TargetCurrency {  get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "1000000000")]
        public decimal Amount { get; set; } = decimal.Zero;
    }
}
