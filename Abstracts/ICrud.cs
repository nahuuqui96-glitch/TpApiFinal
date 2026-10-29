namespace TpApi.Abstracts{
    public interface ICrud<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}