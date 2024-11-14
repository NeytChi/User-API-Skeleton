using LanguageExt.Common;
using Skeleton.Database;
using Skeleton.Requests;

namespace Skeleton.Services
{
    public class UserService : IUserService
    {
        private EmailSender _emailSender;
        private UserValidator _validator = new UserValidator();
        private IUserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        private ILogger<UserService> _logger;

        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _userRepository = repository;
            _logger = logger;
        }
        public Task<Result<User>> SingUp(SingUpRequest request)
        {
            var errors = new List<string>();
            if(_validator.IsOkay(request.Email, out errors) && 
                _validator.IsPasswordTrue(request.Password, request.ConfirmPassword, ref errors))
            {
                var user = _userRepository.GetByEmail(request.Email);
                if (user is null)
                {
                    user = CreateUser(request.Email, request.Password);
                    _userRepository.Add(user);
                    _emailSender.SingUpNotify(user.Email);
                    _logger.LogInformation("Sign up for user with Guid " + user.Id + ".");
                    /// return user;
                }
                if (user.Deleted)
                {
                    user.Deleted = false;
                    _userRepository.Update(user);
                    _logger.LogInformation("Restoring user with Guid " + user.Id + ".");
                }
                // Exit
            }
            _logger.LogInformation("The user with the email " + request.Email + " already exists.");
            throw new NotImplementedException();
        }
        public User CreateUser(string email, string password)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = _validator.ConvertPasswordForStore(password),
                Login = "default",
                CreatedAt = DateTimeOffset.UtcNow,
                Deleted = false,
                Profile = new UserProfile
                {
                    Id = Guid.NewGuid(),
                    FirstName = "",
                    LastName = "",
                    PhoneNumber = "",
                    Birthday = "",
                    Bio = "",
                    Gender = ""
                }
            };
        }
    }
    public interface IUserService
    {
        public Task<Result<User>> SingUp(SingUpRequest request);
    }
}
