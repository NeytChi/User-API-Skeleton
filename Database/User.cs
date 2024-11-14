using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skeleton.Database
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset DeletedAt { get; set; }
        public UserProfile Profile { get; set; }
    }
}
