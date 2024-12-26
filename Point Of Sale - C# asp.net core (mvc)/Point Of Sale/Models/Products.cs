using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class Products
    {
        [Key]
        public string? ProductID { get; set; }

        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? CategoryID { get; set; } 

        [ForeignKey("CategoryID")]
        public ProductCategories? Category { get; set; } 
    }

}
