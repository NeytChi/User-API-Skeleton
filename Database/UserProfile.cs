using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skeleton.Database
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Birthday { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public User user { get; set; }
    }
}
