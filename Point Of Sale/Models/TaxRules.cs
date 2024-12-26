using System.ComponentModel.DataAnnotations;

namespace Point_Of_Sale.Models
{
    public class TaxRules
    {
        [Key]
        public string? TaxRuleID { get; set; }
        public string? TaxName { get; set; }
        public int? TaxRate { get; set; }

        public string? EffectiveDate { get; set; }

        public string? Description { get; set; }
    }
}
