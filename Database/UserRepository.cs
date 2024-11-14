namespace Skeleton.Database
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Update(User user);
        public User GetByEmail(string email);
    }
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _context;
        public UserRepository(ApplicationContext context) 
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            _context.users.Update(user);
            _context.SaveChanges(); 
        }
        public User GetByEmail(string email) => _context.users.Where(u => u.Email == email)
            .FirstOrDefault();
    }
}
