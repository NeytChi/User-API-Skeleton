using Microsoft.EntityFrameworkCore;

namespace Skeleton.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<UserProfile> userProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

}
