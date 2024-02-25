using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DataContext _context;
        private readonly DbSet<T> _table = null;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }




        #region operation
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }


        public void Insert(T entity)
        {
            _table.Add(entity);
        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = GetById(id);
            _table.Remove(existing);
        }
        #endregion

    }
}
