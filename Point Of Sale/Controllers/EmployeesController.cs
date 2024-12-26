using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees ?? new List<Employees>());
        }

        public IActionResult AddNewEmployee()
        {
            ViewBag.Managers = _context.Managers
                                   .Select(m => new SelectListItem
                                   {
                                       Value = m.ManagerID,
                                       Text = m.Employees.FirstName+" "+ m.Employees.LastName
                                   }).ToList();
            return View();
        }

        public async Task<IActionResult> EditEmployee(string? id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Managers = _context.Managers
                                   .Select(m => new SelectListItem
                                   {
                                       Value = m.ManagerID,
                                       Text = m.Employees.FirstName + " " + m.Employees.LastName
                                   }).ToList();

            return View(employee);
        }

        public async Task<IActionResult> ViewEmployee(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee == null)
            {
                return NotFound();
            }

            var manager = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == employee.ManagerID);

            var model = new ManagerEmployeeModel();
            model.Manager = manager;
            model.Employee = employee;

            return View(model);
        }

        public async Task<IActionResult> Add(Employees? employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is required.");
            }

            try
            {
                DateTime now = DateTime.Now;
                string dateString = now.ToString("yyyy-MM-dd HH:mm:ss");

                employee.CreatedAt = dateString;
                employee.UpdatedAt = dateString;

                _context.Employees.Add(employee);

                if (employee.Position?.ToLower() == "manager")
                {
                    var manager = await _context.Managers.FindAsync(employee.EmployeeID);
                    if (manager == null)
                    {
                        Managers mngr = new Managers
                        {
                            EmployeeID = employee.EmployeeID
                        };
                        _context.Managers.Add(mngr);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }catch (Exception ex){
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        public async Task<IActionResult> Edit(Employees? employee)
        {
            if (employee == null)
            {
                return BadRequest("Customer data is required.");
            }

            DateTime now = DateTime.Now;
            string dateString = now.ToString();
            employee.UpdatedAt = dateString;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Customer ID is required.");
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Customer not found.");
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
