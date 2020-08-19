using FactoryDesignPattern.Factory;
using FactoryDesignPattern.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryDesignPattern.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employee;

        public EmployeesController(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        public IActionResult Index()
        {
            var employees = _employee.GetEmployees();

            //var myuser = HttpContext.User;
            //var principal = User as ClaimsPrincipal;
            //var check = User.Identity.IsAuthenticated;

            return View(employees);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var factory = new EmployeeManagerFactory();
            var employeeManager = factory.GetEmployeeManager(employee.EmployeeType);
            employee.Bonus = employeeManager.GetBonus();
            employee.HourlyPay = employeeManager.GetPay();

            var savedEmployee = _employee.AddEmployee(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var employee = _employee.GetEmployee(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(int id, Employee employee)
        {
            var factory = new EmployeeManagerFactory();
            var employeeManager = factory.GetEmployeeManager(employee.EmployeeType);
            employee.Bonus = employeeManager.GetBonus();
            employee.HourlyPay = employeeManager.GetPay();

            _employee.UpdateEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employee.GetEmployee(id);

            if (employee == null)
                return NotFound();

            _employee.DeleteEmployee(id);

            return RedirectToAction("Index");
        }
    }
}