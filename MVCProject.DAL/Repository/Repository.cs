using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private readonly ZuuCargoEntities _context;
        private readonly DbSet<T> _dbset;

        public Repository(ZuuCargoEntities context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return _dbset.AsNoTracking();
        }

        public T GetById(int id)
        {

            return _dbset.Find(id);
        }


        public void Insert(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().AddOrUpdate(entity);

            //_dbset.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;



        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
