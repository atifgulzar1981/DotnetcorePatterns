using FactoryDesignPattern.Models;

namespace FactoryDesignPattern.Services
{
    public interface ISecurityService
    {
        User ValidateCredentials(string userName, string password);
    }
}