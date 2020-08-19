namespace FactoryDesignPattern.Models
{
    public class SqlUserRepository : IUserRepository
    {
        public readonly AppDbContext _dbContext;

        public SqlUserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public User RegisterUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }
    }
}