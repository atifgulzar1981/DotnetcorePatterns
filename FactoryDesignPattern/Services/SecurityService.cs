using System.Linq;
using FactoryDesignPattern.Models;

namespace FactoryDesignPattern.Services
{
    public class SecurityService : ISecurityService
    {
        public readonly AppDbContext _dbContext;

        public SecurityService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User ValidateCredentials(string userName, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == userName);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;

            return null;
        }
    }
}