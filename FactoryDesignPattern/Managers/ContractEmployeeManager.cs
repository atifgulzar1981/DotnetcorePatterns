namespace FactoryDesignPattern.Managers
{
    public class ContractEmployeeManager : IEmployeeManager
    {
        public int GetBonus()
        {
            return 2;
        }

        public int GetPay()
        {
            return 8;
        }
    }
}