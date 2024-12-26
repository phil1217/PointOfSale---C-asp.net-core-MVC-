using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class Employees
    {
        [Key]
        public string? EmployeeID { get; set; }
        public string? ManagerID { get; set; }  

        [ForeignKey("ManagerID")]
        public Managers? Manager { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string? Department { get; set; }
        public string? HireDate { get; set; }
        public int? Salary { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }

}
