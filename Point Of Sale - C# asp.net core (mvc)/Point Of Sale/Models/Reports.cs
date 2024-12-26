using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Point_Of_Sale.Models
{
    public class Reports
    {
        [Key]
        public string? ReportID { get; set; }

        public string? ReportName { get; set; }
        public string? Description { get; set; }
        public string? CreatedAt { get; set; }

        public string? EmployeeID { get; set; }  

        [ForeignKey("EmployeeID")]
        public Employees? GeneratedBy { get; set; }  
    }

}
