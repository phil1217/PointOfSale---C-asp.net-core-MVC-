using System.ComponentModel.DataAnnotations;

namespace Point_Of_Sale.Models
{
    public class ProductCategories
    {
        [Key]
        public string? CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
