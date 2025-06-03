using Domain.Entity;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class AppRepository<T> : IRepository<T> where T : EntityBase
    {
        protected AppDbContext _context;
        protected DbSet<T> _dbSet;

        protected AppRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
    }
}
