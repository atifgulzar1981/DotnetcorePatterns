using FactoryDesignPattern.Managers;
using FactoryDesignPattern.Models;

namespace FactoryDesignPattern.Factory
{
    public class EmployeeManagerFactory
    {
        public IEmployeeManager GetEmployeeManager(EmployeeType employeeType)
        {
            switch (employeeType)
            {
                case EmployeeType.Contract:
                    return new ContractEmployeeManager();
                case EmployeeType.Permanent:
                    return new PermanentEmployeeManager();
                default:
                    return null;
            }
        }
    }
}