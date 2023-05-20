using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Linq.Expressions;
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
        public virtual void Create(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Get(params Func<T, bool>[] where)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            var navigations = _context.Model.FindEntityType(typeof(T)).GetSkipNavigations();
            foreach (var navigation in navigations)
            {
                query = query.Include(navigation.Name);
            }
            foreach (Func<T, bool> func in where)
            {
                query = query.Where(func).AsQueryable();
            }
            return query.OrderBy(o => o.CasVytoreni).Take(50).ToList();
        }
        public T GetById(int id)
        {
            var query = _dbSet.AsQueryable();
            var navigations = _context.Model.FindEntityType(typeof(T)).GetSkipNavigations();
            foreach (var navigation in navigations)
            {
                Console.WriteLine(navigation.Name);
                query = query.Include(navigation.Name);
            }
            return query.FirstOrDefault(e => e.Id == id);
        }

        

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(T entity)
        {
            //Entity framework nepotřebuje tohle
        }
        
    }
}
