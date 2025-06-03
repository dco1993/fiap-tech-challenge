using Domain.Entity;
using Services.DTO;

namespace Services.Mappers
{
    internal static class GameMapper
    {
        internal static Game ToEntity(GameCreateDTO game)
        {
            return new Game
            {
                Title = game.Title,
                Genre = game.Genre,
                Metacritic = game.Metacritic,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate.ToDateTime(TimeOnly.MinValue),
                About = game.About,
                Price = game.Price,
            };
        }

        internal static GameDTO ToDTO(Game game)
        {
            var gameDTO = new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Metacritic = game.Metacritic,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = DateOnly.FromDateTime(game.ReleaseDate),
                About = game.About,
                Price = game.Price,
                Discount = game.CurrentDiscount is null ? null : DiscountMapper.ToDTO(game.CurrentDiscount)
            };

            return gameDTO;
        }

        internal static GameLibraryDTO ToLibraryDTO(Game game)
        {
            var gameLibraryDTO = new GameLibraryDTO
            {
                Id = game.Id,
                Title = game.Title,
                Genre = game.Genre,
                Metacritic = game.Metacritic,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = DateOnly.FromDateTime(game.ReleaseDate),
                About = game.About
            };

            return gameLibraryDTO;
        }
    }
}
