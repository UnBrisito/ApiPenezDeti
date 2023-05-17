using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SpravaPenezDeti.Data
{
    public class Repo<T> : IRepo<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Get()
        {
            return _dbSet.OrderBy(o => o.CasVytoreni).Take(50).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(d => d.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
