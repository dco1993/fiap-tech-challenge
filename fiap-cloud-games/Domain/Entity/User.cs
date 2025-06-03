using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.Entity
{
    public class User : EntityBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("User name is required.");

                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());

                if (value.Length < 5)
                    throw new ArgumentException("User name must contain at least 5 characters.");

                if (value.Length > 100)
                    throw new ArgumentException("User name must contain a maximum of 100 characters.");

                _name = value;
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Email is required.");

                value = value.Trim().ToLower();

                if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("Invalid email.");

                if (value.Length > 100)
                    throw new ArgumentException("User email must contain a maximum of 100 characters.");

                _email = value;
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password is required.");

                value = value.Trim();

                if (!Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$"))
                    throw new ArgumentException("The password must be between 8 and 16 characters long, contain one uppercase letter, one lowercase letter, one number and one special character.");

                _password = value;
            }
        }

        public int IdAccessLevel { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public ICollection<UserGame> UserGame { get; set; }

        override public bool Status { get; set; } = true;
    }
}
