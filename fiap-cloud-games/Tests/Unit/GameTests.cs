using Domain.Entity;
using Bogus;

namespace Tests.Unit
{
    public class GameTests
    {
        private readonly Faker<Game> _gameFaker;

        public GameTests()
        {
            _gameFaker = new Faker<Game>()
                .RuleFor(g => g.Title, f => f.Commerce.ProductName())
                .RuleFor(g => g.Genre, f => f.PickRandom(new[]
                    {
                        "Action", "Adventure", "RPG", "Strategy", "Simulation", "Terror", "City Builder",
                        "Puzzle", "Shooter", "Fighting", "Sports", "Racing", "Horror", "Farming"
                    }))
                .RuleFor(g => g.Metacritic, f => f.Random.Decimal(0, 10))
                .RuleFor(g => g.Publisher, f => f.Random.String2(2, 50))
                .RuleFor(g => g.Developer, f => f.Random.String2(2, 50))
                .RuleFor(g => g.ReleaseDate, f => f.Date.Between(new DateTime(1753, 1, 1), DateTime.Now))
                .RuleFor(g => g.About, f => f.Lorem.Sentence(50))
                .RuleFor(g => g.Price, f => f.Random.Decimal(0, 200));
        }


        #region[Title Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void CreateGameWithoutTitleShouldThrowException(string invalidTitle)
        {
            //Arrange an Act
            var exception = Assert.Throws<ArgumentException>(
            () => _gameFaker.Generate().Title = invalidTitle);

            //Assert
            Assert.Equal("Game title is required.", exception.Message);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abcd")]
        [InlineData("abcd   ")]
        public void CreateGameTitleShortShouldThrowException(string title)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Title = title);

            //Assert
            Assert.Equal("Game title must contain at least 5 characters.", ex.Message);
        }

        [Fact]
        public void CreateGameTitleAboveMaximumShouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _gameFaker.RuleFor(g => g.Title, f => f.Random.String2(101, 110)).Generate());

            //Assert
            Assert.Equal("Game title must contain a maximum of 100 characters.", ex.Message);
        }

        #endregion

        #region[Genre Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void CreateGameWithoutGenreShouldThrowException(string invalidGenre)
        {
            //Arrange an Act
            var exception = Assert.Throws<ArgumentException>(
            () => _gameFaker.Generate().Genre = invalidGenre);

            //Assert
            Assert.Equal("Game genre is required.", exception.Message);
        }

        [Theory]
        [InlineData("   ab")]
        [InlineData("a")]
        [InlineData("b      ")]
        public void CreateGameGenreShortShouldThrowException(string genre)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Genre = genre);

            //Assert
            Assert.Equal("Game genre must contain at least 3 characters.", ex.Message);
        }

        [Fact]
        public void CreateGameGenreAboveMaximumShouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(() =>
                _gameFaker.RuleFor(g => g.Genre, f => f.Random.String2(50, 60)).Generate());

            //Assert
            Assert.Equal("Game genre must contain a maximum of 50 characters.", ex.Message);
        }

        #endregion

        #region[Metacritic Validation]

        [Theory]
        [InlineData(100)]
        [InlineData(11)]
        [InlineData(10.1)]
        public void CreateGameAboveMetacriticScoreShouldThrowsException(decimal score)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Metacritic = score);

            //Assert
            Assert.Equal("Game Metacritic score must be a maximum of 10.", ex.Message);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-10.1)]
        public void CreateGameLowerMetacriticScoreShouldThrowsException(decimal score)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Metacritic = score);

            //Assert
            Assert.Equal("Game Metacritic score must be at least 0.", ex.Message);
        }

        #endregion

        #region[Publisher Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void CreateGameWithoutPublisherShouldThrowException(string publisher)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Publisher = publisher);

            //Assert
            Assert.Equal("Game publisher is required.", ex.Message);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("a  ")]
        [InlineData("   a")]
        [InlineData("   A  ")]
        public void CreateGamePublisherShortShouldThrowException(string invalidPublisher)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.RuleFor(g => g.Publisher, f => invalidPublisher).Generate());

            //Assert
            Assert.Equal("Game publisher must contain at least 2 characters.", ex.Message);
        }

        [Fact]
        public void CreateGamePublisherAboveMaximumShouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.RuleFor(g => g.Publisher, f => f.Random.String2(50, 60)).Generate());

            //Assert
            Assert.Equal("Game publisher must contain a maximum of 50 characters.", ex.Message);
        }

        #endregion

        #region[Developer Validation]

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void CreateGameWithoutDeveloperShouldThrowException(string developer)
        {
            //Arrange an Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Developer = developer);

            //Assert
            Assert.Equal("Game developer is required.", ex.Message);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("a  ")]
        [InlineData("   a")]
        [InlineData("   A  ")]
        public void CreateGameDeveloperShortShouldThrowException(string invalidDeveloper)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.RuleFor(g => g.Developer, f => invalidDeveloper).Generate());

            //Assert
            Assert.Equal("Game developer must contain at least 2 characters.", ex.Message);
        }

        [Fact]
        public void CreateGameDeveloperAboveMaximumShouldThrowException()
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.RuleFor(g => g.Developer, f => f.Random.String2(50, 60)).Generate());

            //Assert
            Assert.Equal("Game developer must contain a maximum of 50 characters.", ex.Message);
        }

        #endregion

        #region[Release Date Validation]

        [Theory]
        [InlineData("1752-12-31")]
        [InlineData("1500-1-12")]
        public void CreateGameWithReleaseDateBeforeAllowedShouldThrowException(string strRelease)
        {
            //Arrange an Act
            var release = DateTime.Parse(strRelease);
            var exception = Assert.Throws<ArgumentException>(
            () => _gameFaker.Generate().ReleaseDate = release);

            //Assert
            Assert.Equal("Release date invalid. Minimum value 01/01/1753.", exception.Message);
        }

        #endregion

        #region[Price Validation]

        [Theory]
        [InlineData(-0.1)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-10.1)]
        public void CreateGameLowerPriceShouldThrowsException(decimal price)
        {
            //Arrange and Act
            var ex = Assert.Throws<ArgumentException>(
                () => _gameFaker.Generate().Price = price);

            //Assert
            Assert.Equal("Game price must be at least 0.", ex.Message);
        }

        #endregion

        [Fact]
        public void CreateValidGameShouldSuccess()
        {
            var game = _gameFaker.Generate();

            Assert.True(game.Status = true);
        }
    }
}
