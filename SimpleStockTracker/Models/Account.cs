using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SimpleStockTracker.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        [DisplayName("Contribution Limit"), DisplayFormat(DataFormatString = "{0:c}")]
        public double? ContributionLimit { get; set; }
        [DisplayName("Account Opening Date")]
        public DateTime OpeningDate { get; set; }
        [Required]
        [DisplayName("Account Holder")]
        public string? AccountHolder { get; set; }

        // add nullable child ref to Holding model
        public List<Holding>? Holdings { get; set; }
    }
}