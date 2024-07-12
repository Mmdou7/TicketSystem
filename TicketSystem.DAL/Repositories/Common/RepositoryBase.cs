using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.DAL.Repositories.Common
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SystemContext _context;
        private readonly DbSet<TEntity> _entities;
        protected RepositoryBase(SystemContext context)
        {
            _entities = context.Set<TEntity>();
            _context = context;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }
        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
