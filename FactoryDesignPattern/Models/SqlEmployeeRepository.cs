using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FactoryDesignPattern.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        public readonly AppDbContext _dbContext;

        public SqlEmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public Employee AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var employee = _dbContext.Employees.Attach(updatedEmployee);
            employee.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return updatedEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }

            return employee;
        }
    }
}