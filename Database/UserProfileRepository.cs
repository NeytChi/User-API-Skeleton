namespace Skeleton.Database
{
    public interface IUserProfileRepository
    {

    }
    public class UserProfileRepository : IUserProfileRepository
    {
        private ApplicationContext _context;
        public UserProfileRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}
