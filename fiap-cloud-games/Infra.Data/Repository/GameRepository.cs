using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class GameRepository : AppRepository<Game>
    {
        public GameRepository(AppDbContext context) : base(context)
        {
        }

        public List<Game> GetAllWithDiscounts()
        {
            var games = _context.Game
                                .Include(g => g.Discounts)
                                .ToList();
            games = games
                        .Select(g =>
                        {
                            g.Discounts = g.Discounts.Select(d => 
                                                     { 
                                                         d.Game = null; 
                                                         return d; 
                                                     }).ToList();
                            return g;
                        })
                        .ToList();

            return games;
        }

        public List<Game> GetLibrary(string email)
        {
            var userLibrary = _context.User
                                      .Where(u => u.Email == email)
                                      .Include(u => u.UserGame)
                                          .ThenInclude(ug => ug.Game)
                                      .SelectMany(u => u.UserGame.Select(ug => ug.Game))
                                      .ToList();

            userLibrary = userLibrary
                          .Select(g =>
                          {
                              g.UserGame = null;
                              return g;
                          })
                          .ToList();

            return userLibrary;
        }
    }
}
