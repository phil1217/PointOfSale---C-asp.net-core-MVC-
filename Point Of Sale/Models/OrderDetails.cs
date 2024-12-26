using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class OrderDetails
    {
        
        public string? ProductID { get; set; }  

      
        [ForeignKey("ProductID")]
        public Products? Product { get; set; }

      
        [Key]
        public string? OrderDetailID { get; set; }

      
        public int? Quantity { get; set; }
        public int? UnitPrice { get; set; }
        public int? TotalPrice { get; set; }

       
        public string? OrderID { get; set; }

   
        [ForeignKey("OrderID")]
        public Orders? Order { get; set; } 
    }

}
