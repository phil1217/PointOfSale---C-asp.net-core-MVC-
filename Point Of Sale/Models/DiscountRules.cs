using System.ComponentModel.DataAnnotations;
namespace Point_Of_Sale.Models
{
    public class DiscountRules
    {
        [Key]
        public string? DiscountRuleID { get; set; }

        public string? DiscountName { get; set; }

        public int? DiscountRate { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public string? Description { get; set; }
    }
}
