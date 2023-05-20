namespace SpravaPenezDeti.Data
{
    public interface IRepo<T>
    {
        bool SaveChanges();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Get(params Func<T, bool>[] where);
    }
}
