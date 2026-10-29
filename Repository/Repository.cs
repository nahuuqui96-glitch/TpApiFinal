using TpApi.Abstracts;

namespace TpApi.Repository{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        IDBContext<T> _context;
        public Repository(IDBContext<T> context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            return _context.Create(entity);
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.GetAll();
        }

        public T GetById(int id)
        {
            return _context.GetById(id);
        }

        public T Update(T entity)
        {
            return _context.Update(entity);
        }
    }
}
