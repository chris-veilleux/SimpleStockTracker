using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace SimpleStockTracker.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        public float? ContributionLimit { get; set; }
        public DateTime OpeningDate { get; set; }
        [Required]
        public string? AccountHolder { get; set; }

        // add nullable child ref to Holding model
        public List<Holding>? Holdings { get; set; }
    }
}
