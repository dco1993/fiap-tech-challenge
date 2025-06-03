using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Domain.Entity
{
    public class Game : EntityBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Game title is required.");

                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());
                
                if (value.Length < 5)
                    throw new ArgumentException("Game title must contain at least 5 characters.");

                if (value.Length > 100)
                    throw new ArgumentException("Game title must contain a maximum of 100 characters.");

                _title = value;
            }
        }

        private string _genre;
        public string Genre
        {
            get => _genre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Game genre is required.");

                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());

                if (value.Length < 3)
                    throw new ArgumentException("Game genre must contain at least 3 characters.");

                if (value.Length > 50)
                    throw new ArgumentException("Game genre must contain a maximum of 50 characters.");

                _genre = value;
            }
        }

        private decimal _metacritic;
        public decimal Metacritic
        {
            get => _metacritic;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Game Metacritic score must be at least 0.");

                if (value > 10)
                    throw new ArgumentException("Game Metacritic score must be a maximum of 10.");

                _metacritic = value;
            }
        }

        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Game publisher is required.");

                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());

                if (value.Length < 2)
                    throw new ArgumentException("Game publisher must contain at least 2 characters.");

                if (value.Length > 50)
                    throw new ArgumentException("Game publisher must contain a maximum of 50 characters.");

                _publisher = value;
            }
        }

        private string _developer;
        public string Developer
        {
            get => _developer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Game developer is required.");

                TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
                value = textInfo.ToTitleCase(value.Trim().ToLower());

                if (value.Length < 2)
                    throw new ArgumentException("Game developer must contain at least 2 characters.");

                if (value.Length > 50)
                    throw new ArgumentException("Game developer must contain a maximum of 50 characters.");

                _developer = value;
            }
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value < new DateTime(1753, 1, 1))
                    throw new ArgumentException("Release date invalid. Minimum value 01/01/1753.");

                _releaseDate = value;
            }
        }

        public string About { get; set; }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Game price must be at least 0.");

                _price = Math.Round(value, 2);
            }
        }

        [NotMapped]
        public Discount CurrentDiscount
        {
            get
            {
                if (Discounts is null || Discounts.Count < 1)
                    return null;

                var discount = Discounts.Where(d => d.Status == true && d.EndDiscount > DateTime.Now)
                                        .OrderByDescending(d => d.Id)
                                        .FirstOrDefault();

                if (discount is null)
                    return null;

                discount.DiscountedPrice = Price - Math.Round(((decimal)discount.PercentOff * Price), 2);

                return discount;
            }
        }

        public ICollection<UserGame> UserGame { get; set; }
        public ICollection<Discount> Discounts { get; set; }

        override public bool Status { get; set; } = true;
    }
}
