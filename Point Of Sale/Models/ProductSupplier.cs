using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class ProductSupplier
    {
        [Key]
        public string? ProductSupplierID { get; set; }
        public string? SupplierID { get; set; }  
        [ForeignKey("SupplierID")]
        public Suppliers? Supplier { get; set; }  
        public int? Price { get; set; }
        public int? QuantityInStock { get; set; }
        public int? ReorderLevel { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }

        public string? ProductID { get; set; }  
        [ForeignKey("ProductID")]
        public Products? Product { get; set; }
    }

}
