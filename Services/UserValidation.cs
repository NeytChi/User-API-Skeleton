namespace Skeleton.Services
{
    public class UserValidator
    {
        public bool IsOkay(string email, out List<string> errors)
        {
            errors = new List<string>();

            // if (string.IsNullOrWhiteSpace(user.Login))
            // {
            //     errors.Add("Login is required.");
            // }

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Email is required.");
            }

            if (!IsValidEmail(email))
            {
                errors.Add("Invalid email format.");
            }

            return errors.Count == 0;
        }
        public bool IsPasswordTrue(string userPassword, string confimedPassword, ref List<string> errors)
        {
            if (!IsValidPassword(userPassword, confimedPassword))
            {
                errors.Add("Password does not meet the required criteria.");
                return false;
            }
            return true;
        }
        public bool IsPasswordStored(string userPassword, string storedHash, ref List<string> errors)
        {
            if (PasswordHelper.VerifyPassword(userPassword, storedHash))
            {
                return true;
            }
            errors.Add("Password is incorrect.");
            return false;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password, string confirmedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return false;
            }
            if (!password.Equals(confirmedPassword))
            {
                return false;
            }
            bool hasUpperChar = false, hasLowerChar = false, hasDigit = false, hasSpecialChar = false;

            foreach (var c in password)
            {
                if (char.IsUpper(c)) hasUpperChar = true;
                if (char.IsLower(c)) hasLowerChar = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (!char.IsLetterOrDigit(c)) hasSpecialChar = true;
            }

            return hasUpperChar && hasLowerChar && hasDigit && hasSpecialChar;
        }
        public string ConvertPasswordForStore(string password)
        {
            return PasswordHelper.GetPasswordForStore(password);
        }
    }
}
