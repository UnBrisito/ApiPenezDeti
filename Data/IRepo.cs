namespace SpravaPenezDeti.Data
{
    public interface IRepo<T>
    {
        bool SaveChanges();
        IEnumerable<T> Get();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
