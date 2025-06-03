using System.Globalization;

namespace Domain.Entity
{
    public class AccessLevel : EntityBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Access Level name is required.");

                if (value.Length < 4)
                    throw new ArgumentException("Access Level name must contain at least 4 characters.");

                if (value.Length > 50)
                    throw new ArgumentException("Access Level name must contain a maximum of 50 characters.");

                _name = value;
            }
        }
        public ICollection<User> User { get; set; }
    }
}
