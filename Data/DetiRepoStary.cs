using System.Linq;

namespace SpravaPenezDeti.Data
{
    public class DetiRepo : IRepo<Dite>
    {
        private readonly AppDbContext _context;

        public DetiRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Dite dite)
        {
            if (dite == null)  throw new ArgumentNullException(nameof(dite)); 
            _context.Deti.Add(dite);
        }

        public void Delete(Dite dite)
        {
            if (dite == null) throw new ArgumentNullException(nameof(dite));
            _context.Deti.Remove(dite);
        }

        public IEnumerable<Dite> Get()
        {
            return _context.Deti.OrderBy(o => o.CasVytoreni).Take(50).ToList();
        }

        public Dite GetById(int id)
        {
            return _context.Deti.FirstOrDefault(d => d.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Update(Dite dite)
        {
            throw new NotImplementedException();
        }
    }
}
