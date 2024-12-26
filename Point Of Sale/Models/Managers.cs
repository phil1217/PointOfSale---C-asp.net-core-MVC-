using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Point_Of_Sale.Models
{
    public class Managers
    {
        private string? _managerId;

        [Key]
        public string? ManagerID
        {
            get => _managerId;
            private set => _managerId = value;
        }

        private string? _employeeId;
        public string? EmployeeID
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                _managerId = value;
            }
        }

        [ForeignKey("EmployeeID")]
        public Employees? Employees { get; set; }

    }

}
