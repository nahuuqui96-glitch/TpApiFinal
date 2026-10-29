using System;
using TpApi.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace TpApi.DataAccess
{
    public class DBContext<T> : IDBContext<T> where T : class, IEntity
    {
        DbSet<T> _table;

        APIDBContext _context;

        public DBContext(APIDBContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public T Create(T entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            T table = _table.Where(item => item.Id == id).FirstOrDefault()!;
            
            if (table != null)
            {
                _context.Remove<T>(table!);
                _context.SaveChanges();
            }
        }

        public IQueryable<T> GetAll()
        {
            return _table.AsQueryable<T>();
        }

        public T GetById(int id)
        {
            return _table.Where(item => item.Id.Equals(id)).FirstOrDefault()!;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
