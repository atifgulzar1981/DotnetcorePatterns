using System.ComponentModel.DataAnnotations;

namespace FactoryDesignPattern.Models
{
    public enum EmployeeType
    {
        Contract = 1,
        Permanent = 2
    }

    public class Employee
    {
        public int Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        public int HourlyPay { get; set; }
        public int Bonus { get; set; }

        [Required] public EmployeeType EmployeeType { get; set; }
    }
}