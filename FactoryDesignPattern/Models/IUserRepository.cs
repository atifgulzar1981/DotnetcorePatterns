namespace FactoryDesignPattern.Models
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User RegisterUser(User user);

    }
}