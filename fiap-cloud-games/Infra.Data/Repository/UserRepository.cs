using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class UserRepository : AppRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public User GetByName(string name)
        {
            var user = _context.User
                .AsNoTracking()
                .FirstOrDefault(u => u.Name == name);

            return user;
        }

        public User GetByEmail(string email)
        {
            var users = _context.User
                .AsNoTracking()
                .Include(u => u.AccessLevel)
                .ToList();

            var user = users
                .Where(u => u.Email == email)
                .Select(u =>
                {
                    u.AccessLevel.User = null;
                    return u;
                })
                .FirstOrDefault();

            return user;
        }

        public new User GetById(int id)
        {
            var users = _context.User
                .AsNoTracking()
                .Include(u => u.AccessLevel)
                .ToList();

            var user = users
                .Where(u => u.Id == id)
                .Select(u =>
                {
                    u.AccessLevel.User = null;
                    return u;
                })
                .FirstOrDefault();

            return user;
        }

        public User UpdateAccessLevel(int userId, int accessLevelId)
        {
            _context.User
                    .Where(u => u.Id == userId)
                    .ExecuteUpdate(s => s.SetProperty(u => u.IdAccessLevel, accessLevelId));

            _context.SaveChanges();

            return GetById(userId);
        }

    }
}
