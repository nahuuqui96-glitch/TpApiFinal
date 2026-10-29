using TpApi.Abstracts;

namespace TpApi.Application
{
    public class Application<T> : IApplication<T> where T : IEntity
    {
        IRepository<T> _repository;

        public Application(IRepository<T> repository)
        {
            _repository = repository;
        }
        public T Create(T entity)
        {
            return _repository.Create(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}