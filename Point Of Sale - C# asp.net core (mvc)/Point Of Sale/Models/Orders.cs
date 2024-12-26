using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class Orders
    {
      
        [Key]
        public string? OrderID { get; set; }

        public string? CustomerID { get; set; }  

        [ForeignKey("CustomerID")]
        public Customers? Customer { get; set; }  
        public string? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public int? TotalAmount { get; set; }
        public int? TaxAmount { get; set; }
        public int? DiscountAmount { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }

}
