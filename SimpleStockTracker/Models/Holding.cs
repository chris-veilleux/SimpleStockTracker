using System.ComponentModel.DataAnnotations;

namespace SimpleStockTracker.Models
{
    public class Holding
    {
        public int HoldingId { get; set; }
        [Required]
        [MaxLength(5)]
        public string? Ticker { get; set; }
        [Required]
        public DateTime TradeDate { get; set; }
        [Required]
        [MaxLength(4)]
        public string? TradeType { get; set; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int Quantity { get; set; }
        [Required]
        [Range(0,1000000, ErrorMessage = "That price is invalid - share price should be in the range 0-1000000.")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Price { get; set; }

        // foreign key for parent account
        [Display(Name = "Account Name")]
        public int AccountId { get; set; }

        // parent reference for auto-joins
        public Account? Account { get; set; }
    }
}
