namespace FactoryDesignPattern.Managers
{
    public class PermanentEmployeeManager : IEmployeeManager
    {
        public int GetBonus()
        {
            return 5;
        }

        public int GetPay()
        {
            return 10;
        }
    }
}